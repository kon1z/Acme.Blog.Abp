﻿using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Xunit;

namespace Acme.Blog.Samples;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class SampleAppServiceTests<TStartupModule> : BlogApplicationTestBase<TStartupModule>
	where TStartupModule : IAbpModule
{
	private readonly IIdentityUserAppService _userAppService;

	protected SampleAppServiceTests()
	{
		_userAppService = GetRequiredService<IIdentityUserAppService>();
	}

	[Fact]
	public async Task Initial_Data_Should_Contain_Admin_User()
	{
		//Act
		var result = await _userAppService.GetListAsync(new GetIdentityUsersInput());

		//Assert
		result.TotalCount.ShouldBeGreaterThan(0);
		result.Items.ShouldContain(u => u.UserName == "admin");
	}
}