using Volo.Abp.Threading;

namespace Acme.Blog;

public static class BlogDtoExtensions
{
	private static readonly OneTimeRunner OneTimeRunner = new();

	public static void Configure()
	{
		OneTimeRunner.Run(() =>
		{
			/* You can add extension properties to DTOs
			 * defined in the depended modules.
			 *
			 * Example:
			 *
			 * ObjectExtensionManager.Instance
			 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
			 *
			 * See the documentation for more:
			 * https://docs.abp.io/en/abp/latest/Object-Extensions
			 */
		});
	}
}