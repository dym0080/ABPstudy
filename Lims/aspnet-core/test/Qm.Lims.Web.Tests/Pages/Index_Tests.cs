using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Qm.Lims.Pages
{
    public class Index_Tests : LimsWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
