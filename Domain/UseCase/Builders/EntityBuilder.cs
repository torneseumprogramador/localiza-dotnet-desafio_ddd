using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Authentication;
using Domain.Entities;
using Domain.ViewModel;
using Domain.ViewModel.Jwt;
using Infrastructure.Services;
using Infrastructure.Services.Person;

namespace Domain.UseCase.Builders
{
    public class EntityBuilder
    {
        public static T Call<T>(object userSave)
        {
            var entity = Activator.CreateInstance<T>();
            foreach (var field in userSave.GetType().GetProperties())
            {
                var value = userSave.GetType().GetProperty(field.Name).GetValue(userSave);
                if(value != null)
                    entity.GetType().GetProperty(field.Name).SetValue(entity, value);
            }

            return entity;
        }
    }
}
