namespace Test_NickVn
{
    public class HomeControllerTest
    {
        private static DbContextOptions<NickVn_ProjectContext> dbContextOptions = new DbContextOptionsBuilder<NickVn_ProjectContext>()
            .UseMySql("server=mysql.giang.cf;user id=giang_vtca;password=1@giangvtcacademy;port=3306;database=NickVn_Project_test;Convert Zero Datetime=True;", ServerVersion.Parse("10.4.22-mariadb"))
            .Options;

        private static NickVn_ProjectContext context = new NickVn_ProjectContext(dbContextOptions);

        [Fact]
        public async Task HomeControllerIndexTest()
        {
            HomeController homeCtrl = new HomeController((new NullLogger<HomeController>()), context);

            var res = await homeCtrl.Index() as ViewResult;

            Assert.Equal("Index", res.ViewName);
        }

        [Theory]
        [InlineData(1, "Garena")]
        [InlineData(2, "Garena")]
        [InlineData(null, "Garena")]
        public async Task Category_GarenaTest(int? id, string? view)
        {
            CategoryController cateCtrl = new CategoryController((new NullLogger<CategoryController>()), context);

            var index = cateCtrl.Index(id) as ViewResult;

            var garena = await cateCtrl.Garena(id) as ViewResult;
            var vb = garena.ViewData["cateProduct"];
            System.Console.WriteLine(vb);

            Assert.Equal(view, garena.ViewName);
        }

    }
}