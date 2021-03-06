﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HowToWebApplication.Helpers;
using static HowToWebApplication.Helpers.PasswordHelper;

namespace HowToWebApplication.Models
{
    public class UsersDataProvider
    {
        HowToDbEntities _db = new HowToDbEntities();


        public bool Exist(UsersCustomClass user)
        {
            return _db.users.FirstOrDefault(e => e.email == user.Email) == null ? false : true;
        }

        public users GetUserById(int id)
        {
            return _db.users.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateUser(UsersCustomClass user)
        {
            DateTime localDate = DateTime.Now;
            if (!Exist(user))
            {
                _db.users.Add(new users()
                {
                    name = user.Name,
                    surname = user.Surname,
                    email = user.Email,
                    password = SHA.GenerateSHA512String(user.Password),
                    loginDate= localDate,
                    isActive = user.IsActive,
                    categoriesId = user.CategoriesId,
                });
            }
            _db.SaveChanges();
        }

        //Edit 
        public void EditUser(UsersCustomClass user)
        {
            var result = _db.users.FirstOrDefault(e => e.Id == user.Id);
            if (!Exist(user) || result.email == user.Email)
            {
                result.name = user.Name;
                result.surname = user.Surname;
                result.email = user.Email;
                result.password = SHA.GenerateSHA512String(user.Password);
                result.isActive = user.IsActive;
                result.categoriesId = user.CategoriesId;
            }
            _db.SaveChanges();
        }

        //Delete
        //public void deleteUser(Users user)
        //{
        //    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
        //    result.IsActive = false;
        //    _db.SaveChanges();
        //}


        public void FullDeleteUser(users user)
        {
            var deleteRequest = _db.requests.Where(e => e.usersId == user.Id);
            var deleteImage = _db.images.Where(e => e.usersId == user.Id);
            var deleteFavourite = _db.favourites.Where(e => e.usersId == user.Id);
            var deleteArticle = _db.articles.Where(e => e.usersId == user.Id);
            var deleteArticleTags = _db.articlesTags.Where(e => e.articles.usersId == user.Id);
            var deleteArticleCategories = _db.articleCategories.Where(e => e.articles.usersId == user.Id);
            var deleteUsers = _db.users.Where(e => e.Id == user.Id);


            _db.requests.RemoveRange(deleteRequest);
            _db.images.RemoveRange(deleteImage);
            _db.favourites.RemoveRange(deleteFavourite);
            _db.articlesTags.RemoveRange(deleteArticleTags);
            _db.articles.RemoveRange(deleteArticle);
            _db.articleCategories.RemoveRange(deleteArticleCategories);            
            _db.users.RemoveRange(deleteUsers);

            _db.SaveChanges();
        }

        //search 
        public IEnumerable<users> GetUsers(string name, string surname, string mail)
        {

            return _db.users.Where(e => e.name.Contains(name) && e.surname.Contains(surname) && e.email.Contains(mail));
        }

        public IEnumerable<users> AllUsers()
        {
            return _db.users;
        }

        public enum categories
        {
            Administrator = 1,
            Moderator = 2,
            User = 3
        }
        public usersCategories GetUserCategory()
        {
            return _db.usersCategories.FirstOrDefault(e => e.Id == (int)categories.User);
        }
    }
}