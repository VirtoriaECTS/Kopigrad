using Kopigrad.Models;

namespace Kopigrad.Components.Classes.Autorisation
{
    public class AutorisationClasses
    {
        private string code = "vbFHnc7t-A-Md.";

        public void register(string nameUser, string email, string password, string code)
        {
            using(var context = new KopigradContext())
            {
                Classes.Autorisation.Hash hash = new Classes.Autorisation.Hash();
                hash.Create(password);
                string hashpawword = hash.HashValue;
                string salt = hash.Salt;


                var user = new Models.User()
                {
                    NameUser = nameUser,
                    Email = email,
                    HashPassworld = hashpawword,
                    Salt = salt
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }


        public void changePassword(string email, string password)
        {
            using (var context = new KopigradContext())
            {
                Classes.Autorisation.Hash hash = new Classes.Autorisation.Hash();
                hash.Create(password);
                string hashpawword = hash.HashValue;
                string salt = hash.Salt;


                var user = context.Users.Where(x => x.Email == email).FirstOrDefault();

                user.HashPassworld = hashpawword;
                user.Salt = salt;

                


                context.SaveChanges();
            }
        }


        public string CheakPassword(string email, string password)
        {
            using (var context = new KopigradContext())
            {
                var user = context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (user != null)
                {
                    Classes.Autorisation.Hash hash = new Classes.Autorisation.Hash();
                    hash.HashValue = user.HashPassworld;
                    hash.Salt = user.Salt;

                    bool cheak = hash.Verify(password);
                    if (cheak)
                    {
                        return "";
                    }
                    else
                    {
                        return "Неверный пароль";
                    }
                }
                else
                {
                    return "Пользователь с данной почтой не найден";
                }
                
            }
        }
    }
}
