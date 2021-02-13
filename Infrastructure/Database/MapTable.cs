using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Infrastructure.Database
{
    public class MapTable
    {
        public static string BuilderInsert<T>(T entity)
        {
            var name = $"{entity.GetType().Name.ToLower()}s";
            var table = entity.GetType().GetCustomAttribute<TableAttribute>();
            if (table != null && !string.IsNullOrEmpty(table.Name))
            {
                name = table.Name;
            }

            var fields = entity.GetType().GetProperties();

            var sql = $"insert into {name} (";
            List<string> colsDb = new List<string>();
            List<string> colsDbParameter = new List<string>();

            foreach (var field in fields)
            {
                var persistedField = field.GetCustomAttribute<ColumnAttribute>();
                var pkField = field.GetCustomAttribute<KeyAttribute>();
                if (persistedField != null)
                {
                    if (field.GetValue(entity) != null && pkField == null)
                    {
                        var nameField = string.IsNullOrEmpty(persistedField.Name) ? field.Name : persistedField.Name;
                        colsDb.Add(nameField);
                        colsDbParameter.Add($"@{nameField}");
                    }
                }
            }

            sql += string.Join(",", colsDb.ToArray());

            sql += ") values (";

            sql += string.Join(",", colsDbParameter.ToArray());
            sql += ") SELECT SCOPE_IDENTITY()";

            return sql;
        }

        public static void SetIdOfEntity<T>(T entity, object value)
        {
            var fields = entity.GetType().GetProperties();
            foreach (var field in fields)
            {
                var pkAttr = field.GetCustomAttribute<KeyAttribute>();
                if (pkAttr != null)
                {
                    field.SetValue(entity, Convert.ToInt32(value));
                    break;
                }
            }
        }

        public static T CreateInstanceAndSetId<T>(int id)
        {
            var entity = Activator.CreateInstance(typeof(T));
            var fields = entity.GetType().GetProperties();
            foreach (var field in fields)
            {
                var pkAttr = field.GetCustomAttribute<KeyAttribute>();
                if (pkAttr != null)
                {
                    field.SetValue(entity, id);
                    break;
                }
            }
            return (T)entity;
        }

        public static string BuilderUpdate<T>(T entity)
        {
            var name = $"{entity.GetType().Name.ToLower()}s";
            var table = entity.GetType().GetCustomAttribute<TableAttribute>();
            if (table != null && !string.IsNullOrEmpty(table.Name))
            {
                name = table.Name;
            }

            var fields = entity.GetType().GetProperties();

            var sql = $"update {name} set ";
            List<string> colsDb = new List<string>();

            PropertyInfo pkProperty = null;
            foreach (var field in fields)
            {
                var pkAttr = field.GetCustomAttribute<KeyAttribute>();
                if (pkAttr != null) pkProperty = field;

                var persistedField = field.GetCustomAttribute<ColumnAttribute>();
                var pkField = field.GetCustomAttribute<KeyAttribute>();
                if (persistedField != null && pkField == null)
                {
                    var nameField = string.IsNullOrEmpty(persistedField.Name) ? field.Name : persistedField.Name;
                    if (field.GetValue(entity) != null)
                        colsDb.Add($"{nameField}=@{nameField}");
                }
            }

            sql += string.Join(",", colsDb.ToArray());

            if (pkProperty == null) throw new Exception("Esta entidade não foi definida uma chave primário, coloque o atributo [Pk]");

            var cAttr = pkProperty.GetCustomAttribute<ColumnAttribute>();
            var columnName = (cAttr != null && !string.IsNullOrEmpty(cAttr.Name)) ? cAttr.Name : pkProperty.Name;;

            sql += $" where {columnName}=@{columnName}";

            return sql;
        }

        public static string BuilderSelect<T>(string sqlWhere = null)
        {
            var entity = Activator.CreateInstance<T>();
            var name = $"{entity.GetType().Name.ToLower()}s";
            var table = entity.GetType().GetCustomAttribute<TableAttribute>();
            if (table != null && !string.IsNullOrEmpty(table.Name))
            {
                name = table.Name;
            }
            if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere = $" {sqlWhere}";

            return $"select {name}.* from {name}{sqlWhere}";
        }

        public static string BuilderDelete<T>(T entity)
        {
            var name = $"{entity.GetType().Name.ToLower()}s";
            var table = entity.GetType().GetCustomAttribute<TableAttribute>();
            if (table != null && !string.IsNullOrEmpty(table.Name))
            {
                name = table.Name;
            }

            var fields = entity.GetType().GetProperties();

            var sql = $"delete from {name}";
            List<string> colsDb = new List<string>();

            PropertyInfo pkProperty = null;
            foreach (var field in fields)
            {
                var pkAttr = field.GetCustomAttribute<KeyAttribute>();
                if (pkAttr != null)
                {
                    pkProperty = field;
                    break;
                }
            }

            if (pkProperty == null) throw new Exception("Esta entidade não foi definida uma chave primário, coloque o atributo [Pk]");

            var value = Convert.ToInt32(pkProperty.GetValue(entity));
            var cAttr = pkProperty.GetCustomAttribute<ColumnAttribute>();
            var columnName = (cAttr != null && !string.IsNullOrEmpty(cAttr.Name)) ? cAttr.Name : pkProperty.Name;;
            sql += $" where {columnName}={columnName}";

            return sql;
        }

        public static string BuildFindById<T>(int id)
        {
            var name = $"{typeof(T).Name.ToLower()}s";
            var table = typeof(T).GetCustomAttribute<TableAttribute>();
            if (table != null && !string.IsNullOrEmpty(table.Name))
            {
                name = table.Name;
            }

            var fields = typeof(T).GetProperties();

            var sql = $"select * from {name}";

            PropertyInfo pkProperty = null;
            foreach (var field in fields)
            {
                var pkAttr = field.GetCustomAttribute<KeyAttribute>();
                if (pkAttr != null)
                {
                    pkProperty = field;
                    break;
                }
            }

            if (pkProperty == null) throw new Exception("Esta entidade não foi definida uma chave primário, coloque o atributo [Pk]");

            var cAttr = pkProperty.GetCustomAttribute<ColumnAttribute>();
            var columnName = (cAttr != null && !string.IsNullOrEmpty(cAttr.Name)) ? cAttr.Name : pkProperty.Name;
            sql += $" where {columnName}={id}";

            return sql;
        }

        public static SqlParameter GetBuilderValue<T>(T obj, string sqlParameter, string objPropriety)
        {
            var value = obj.GetType().GetProperty(objPropriety).GetValue(obj);
            if (value == null) return null;
            var param = new SqlParameter(sqlParameter, GetDbType(value));
            param.Value = value;
            return param;
        }

        public static SqlDbType GetDbType(object value)
        {
            var result = SqlDbType.VarChar;
            Type type = value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Object:
                    result = SqlDbType.Variant;
                    break;
                case TypeCode.Boolean:
                    result = SqlDbType.Bit;
                    break;
                case TypeCode.Char:
                    result = SqlDbType.NChar;
                    break;
                case TypeCode.SByte:
                    result = SqlDbType.SmallInt;
                    break;
                case TypeCode.Byte:
                    result = SqlDbType.TinyInt;
                    break;
                case TypeCode.Int16:
                    result = SqlDbType.SmallInt;
                    break;
                case TypeCode.UInt16:
                    result = SqlDbType.Int;
                    break;
                case TypeCode.Int32:
                    result = SqlDbType.Int;
                    break;
                case TypeCode.UInt32:
                    result = SqlDbType.BigInt;
                    break;
                case TypeCode.Int64:
                    result = SqlDbType.BigInt;
                    break;
                case TypeCode.UInt64:
                    result = SqlDbType.Decimal;
                    break;
                case TypeCode.Single:
                    result = SqlDbType.Real;
                    break;
                case TypeCode.Double:
                    result = SqlDbType.Float;
                    break;
                case TypeCode.Decimal:
                    result = SqlDbType.Money;
                    break;
                case TypeCode.DateTime:
                    result = SqlDbType.DateTime;
                    break;
                case TypeCode.String:
                    result = SqlDbType.VarChar;
                    break;
            }

            return result;
        }

        public static List<SqlParameter> BuilderParameters<T>(T obj, bool includePk = false)
        {
            var fields = obj.GetType().GetProperties();

            List<SqlParameter> parameters = new List<SqlParameter>();

            foreach (var field in fields)
            {
                var pkField = field.GetCustomAttribute<KeyAttribute>();
                if (includePk)
                {
                    if (pkField != null)
                    {
                        var cAttr = field.GetCustomAttribute<ColumnAttribute>();
                        var nameField = (cAttr != null && !string.IsNullOrEmpty(cAttr.Name)) ? cAttr.Name : field.Name;

                        var parameter = GetBuilderValue(obj, $"@{nameField}", field.Name);
                        if (parameter != null)
                            parameters.Add(parameter);
                    }
                }

                var persistedField = field.GetCustomAttribute<ColumnAttribute>();
                if (persistedField != null && pkField == null)
                {
                    var nameField = string.IsNullOrEmpty(persistedField.Name) ? field.Name : persistedField.Name;
                    var parameter = GetBuilderValue(obj, $"@{nameField}", field.Name);
                    if (parameter != null)
                        parameters.Add(parameter);
                }
            }

            return parameters;
        }
    }
}