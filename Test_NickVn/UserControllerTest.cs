using Project_Sem2_WD07_NickVn.Models;

namespace Test_NickVn
{
    public class UserControllerTest
    {
        // Arrange
        private static readonly DbContextOptions<NickVn_ProjectContext> dbContextOptions = new DbContextOptionsBuilder<NickVn_ProjectContext>()
            .UseMySql("server=mysql.giang.cf;user id=giang_vtca;password=1@giangvtcacademy;port=3306;database=NickVn_Project_test;Convert Zero Datetime=True;", ServerVersion.Parse("10.4.22-mariadb"))
            .Options;

        private static readonly NickVn_ProjectContext context = new(dbContextOptions);
        private static readonly NullLogger<UserController> logger = new();
        private static readonly UserController userController = new(logger, context);

        [Theory]
        [InlineData(true ,"giangadmin", "Login", "Index")]
        [InlineData(false ,"giangadmin", "Login", "Login")]
        [InlineData(false , "admin8", "1", "Index")]
        [InlineData(false, null, null, "Login")]
        public async Task LoginSolveTest(bool isLogged, string? userName, string? password, string expected)
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var httpContext = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            if (isLogged)
            {
                httpContext.Session.SetInt32("_Id", 1);
            }

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            UserController ctrl = new(logger, context)
            {
                ControllerContext = new ControllerContext(),
                TempData = tempData
            };
            ctrl.ControllerContext.HttpContext = httpContext;
            ctrl.ControllerContext.HttpContext.Session.SetInt32("_Id", 1);

            // Act
            var result = await ctrl.LoginSolve(userName, password);
            var viewRes = result as ViewResult;
            var redirectRes = result as RedirectToActionResult;

            // Assert
            //if(viewRes != null)
            //    Assert.Equal(expected, viewRes.ViewName);
            //else if (redirectRes != null)
            //    Assert.Equal(expected, redirectRes.ActionName);

            Assert.True(ctrl.HttpContext.Session.GetInt32("_Id").Equals(1));

        }

        [Theory]
        [InlineData(1, 10000000, "giangadmin", "Login")]
        [InlineData(null, null, null, "Login")]
        public async Task IndexTestRedirectResult(int? userId, int? money, string? userName, string expected)
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var httpContext = new DefaultHttpContext { };

            if (userId is not null && money is not null && userName is not null)
            {
                httpContext.Session = sessionMock.Object;
                httpContext.Session.SetInt32("_Id", (int)userId);
                httpContext.Session.SetInt32("_Money", (int)money);
                httpContext.Session.SetString("_Name", userName);
            }

            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var ctrl = new UserController(logger, context)
            {
                ControllerContext = new ControllerContext(),
                TempData = tempData
            };
            ctrl.ControllerContext.HttpContext = httpContext;

            // Act
            var res = await userController.Index() as RedirectToActionResult;

            // Assert
            Assert.Equal(expected, res.ActionName);
        }

    }
}