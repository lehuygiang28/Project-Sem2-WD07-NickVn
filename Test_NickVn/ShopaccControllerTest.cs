namespace Test_NickVn
{
    public class ShopaccControllerTest
    {
        // Arrange
        private static readonly DbContextOptions<NickVn_ProjectContext> dbContextOptions = new DbContextOptionsBuilder<NickVn_ProjectContext>()
            .UseMySql("server=mysql.giang.cf;user id=giang_vtca;password=1@giangvtcacademy;port=3306;database=NickVn_Project_test;Convert Zero Datetime=True;", ServerVersion.Parse("10.4.22-mariadb"))
            .Options;

        private static readonly NickVn_ProjectContext context = new(dbContextOptions);
        private static readonly NullLogger<ShopaccController> logger = new();
        private static readonly ShopaccController shopCtrl = new(logger, context);

        [Theory]
        [InlineData(null, "LienMinh", 1, 100000, "Giang")]
        [InlineData(1, "LienMinh", null, null, null)]
        [InlineData(5, "LienMinh", 2, 500000, "Admin")]
        [InlineData(-00099, "LienMinh", 3, 700000, "Mod")]
        [InlineData(00099, "LienMinh", 4, 322002, "Giang")]
        //[InlineData(3460, "Index", 5, 900000000, "admin8")]
        public async Task LienMinhBuyConfirmSolveWithLoginTest(int? productId, string? expected, int? userId, int? money, string? userName)
        {
            // Arrange
            var sessionMock = new Mock<ISession>();
            var httpContext = new DefaultHttpContext
            {
                Session = sessionMock.Object
            };

            if(userId is not null && money is not null && userName is not null)
            {
                httpContext.Session.SetInt32("_Id", (int)userId);
                httpContext.Session.SetInt32("_Money", (int)money);
                httpContext.Session.SetString("_Name", (string)userName);
            }
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var ctrl = new ShopaccController(logger, context)
            {
                TempData = tempData,
                ControllerContext = new ControllerContext()
            };
            ctrl.ControllerContext.HttpContext = httpContext;


            // Act
            var result = await ctrl.BuyConfirmSolve(productId);

            // Assert
            if (result is RedirectToActionResult redirectRes)
            {
                Assert.Equal(expected, redirectRes.ActionName);
            }
            else if (result is ViewResult viewRes)
            {
                Assert.Equal(expected, viewRes.ViewName);
            }
            else
            {
                Assert.NotNull(result);
            }
        }


        [Theory]
        [InlineData(null, "LienMinh")]
        [InlineData(1, "LienMinh")]
        [InlineData(5, "LienMinh")]
        [InlineData(00099, "LienMinh")]
        [InlineData(-00099, "LienMinh")]
        public async Task LienMinhBuyConfirmSolveWithoutLoginTest(int? productId, string? expected)
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            var ctrl = new ShopaccController(logger, context) { TempData = tempData };

            // Act
            var result = await ctrl.BuyConfirmSolve(productId);

            // Assert
            if (result is RedirectToActionResult redirectRes)
            {
                Assert.Equal(expected, redirectRes.ActionName);
            }
            else if (result is ViewResult viewRes)
            {
                Assert.Equal(expected, viewRes.ViewName);
            }
            else
            {
                Assert.NotNull(result);
            }
        }


        [Theory]
        [InlineData(null, "LienMinh")]
        [InlineData(1, "LienMinh")]
        [InlineData(2, "LienMinh")]
        [InlineData(75, "Detail_LienMinh")]
        [InlineData(3460, "Detail_LienMinh")]
        [InlineData(-3460, "LienMinh")]
        [InlineData(998989, "LienMinh")]
        public async Task LienMinhDetailsTest(int? id, string? viewNamePredict)
        {
            // Arrange

            // Act
            var result = await shopCtrl.Detail_LienMinh(id);

            // Assert
            if (result is RedirectToActionResult redirectRes)
            {
                Assert.Equal(viewNamePredict, redirectRes.ActionName);
            }
            else if (result is ViewResult viewRes)
            {
                Assert.Equal(viewNamePredict, viewRes.ViewName);
            }
            else
            {
                Assert.NotNull(result);
            }
        }

        [Theory]
        [InlineData(null, 10)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 8)]
        [InlineData(6, 6)]
        [InlineData(7, 4)]
        [InlineData(99, 10)]
        [InlineData(int.MaxValue, 10)]
        [InlineData(int.MinValue, 10)]
        public async Task LienMinhSearchPriceTest(int? price, int? totalPagePredict)
        {
            // Arrange

            // Act
            var res = await shopCtrl.LienMinh(null, null, null, price, null, null) as ViewResult;
            int totalPageInView = (int)res.ViewData["totalPage"];

            // Assert
            Assert.Equal(totalPagePredict, totalPageInView);
        }


        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(66, 1)]
        [InlineData(null, 10)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        [InlineData(6, 0)]
        [InlineData(int.MaxValue, 0)]
        [InlineData(int.MinValue, 0)]
        public async Task LienMinhSearchIdTest(int? id, int? totalPagePredict)
        {
            // Arrange

            // Act
            var res = await shopCtrl.LienMinh(null, null, id, null, null, null) as ViewResult;
            int totalPageInView = (int)res.ViewData["totalPage"];

            // Assert
            Assert.Equal(totalPagePredict, totalPageInView);
        }


        [Theory]
        [InlineData(null, null, null, null, null, null, 10)]
        [InlineData(null, "cao", null, null, null, null, 3)]
        [InlineData(null, "chua", null, null, null, null, 1)]
        [InlineData(null, "99", null, null, null, null, 0)]
        [InlineData(null, "99999999999999999", null, null, null, null, 0)]
        [InlineData(null, "bach", null, null, null, null, 1)]
        [InlineData(null, "gia g g", null, null, null, null, 0)]
        [InlineData(null, "!@#@!#@!#", null, null, null, null, 0)]
        public async Task LienMinhSearchKeyTest(int? page, string? SearchKey, int? id, int? price, int? status, int? sort_price, int? totalPagePredict)
        {
            // Arrange

            // Act
            var res = await shopCtrl.LienMinh(page, SearchKey, id, price, status, sort_price) as ViewResult;
            int totalPageInView = (int)res.ViewData["totalPage"];

            // Assert
            Assert.Equal(totalPagePredict, totalPageInView);
        }

        [Theory]
        [InlineData(null, null, null, null, null, null, 1)]
        [InlineData(1, null, null, null, null, null, 1)]
        [InlineData(4, null, null, null, null, null, 4)]
        [InlineData(9, null, null, null, null, null, 9)]
        [InlineData(10, null, null, null, null, null, 10)]
        [InlineData(11, null, null, null, null, null, 10)]
        [InlineData(100, null, null, null, null, null, 10)]
        public async Task LienMinhSelectPageTest(int? page, string? SearchKey, int? id, int? price, int? status, int? sort_price, int? pageSelectPredict)
        {
            // Arrange

            // Act
            var res = await shopCtrl.LienMinh(page, SearchKey, id, price, status, sort_price) as ViewResult;
            int? pageSelectInView = (int)res.ViewData["currentPage"];

            // Assert
            Assert.Equal(pageSelectPredict, pageSelectInView);
        }

        [Fact]
        public async Task LienMinhViewNameTest()
        {
            // Arrange

            // Act
            var res = await shopCtrl.LienMinh(null, null, null, null, null, null) as ViewResult;

            // Assert
            Assert.Equal("LienMinh", res.ViewName);
        }

    }
}