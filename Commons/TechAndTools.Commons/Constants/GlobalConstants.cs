namespace TechAndTools.Commons.Constants
{
    public static class GlobalConstants
    {
        //root admin
        public const string AdminUsername = "admin";
        public const string AdminFirstName = "Tihomir";
        public const string AdminLastName = "Vasilev";
        public const string AdminEmail = "techandtoolsbg@gmail.com";
        public const string AdminPassword = "asdasd";
        public const string AdminPhoneNumber = "+359999999999";

        //cloudinary folder
        public const string StorageFolder = "TechAndTools_images";

        //order statuses
        public const string Processed = "Обработена";
        public const string Unprocessed = "Необработена";
        public const string Delivered = "Доставена";

        //payment statuses
        public const string Paid = "Платена";
        public const string Unpaid = "Неплатена";

        //payment types
        public const string CashOnDeliver = "Наложен платеж";
        
        //Areas
        public const string AdministrationArea = "Administration";
        public const string BlogArea = "Blog";

        //Roles
        public const string AdminRole = "Admin";
        public const string UserRole = "User";

        //Shopping Cart
        public const string SessionShoppingCartKey = "shoppingCart";

        public const string AuthCookieName = "AuthCookie";

        public const int DefaultProductQuantity = 1;
    }
}
