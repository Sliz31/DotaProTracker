namespace DotaProTracker.Models
{
    public static class UserStore
    {
        // Список для хранения зарегистрированных пользователей
        private static List<Person> _users = new List<Person>();

        public static Person? CurrentUser { get; set; }

        // Метод для добавления пользователя
        public static void AddUser(Person person)
        {
            _users.Add(person);
        }

        // Метод для проверки пользователя по (email или nickname) и паролю
        public static Person? ValidateUser(string emailOrNickname, string password)
        {
            return _users.Find(user => (user.Email == emailOrNickname || user.Nickname == emailOrNickname) && user.Password == password);
        }

        // Метод для получения текущего пользователя
        public static Task<Person?> GetCurrentUser()
        {
            return Task.FromResult(CurrentUser);
        }

        // Метод для выхода из системы
        public static Task Logout()
        {
            CurrentUser = null;
            return Task.CompletedTask;
        }
    }
}
