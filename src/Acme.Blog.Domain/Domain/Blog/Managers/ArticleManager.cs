using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using JetBrains.Annotations;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Acme.Blog.Domain.Blog.Managers;

[UsedImplicitly]
public class ArticleManager : DomainService
{
    public IArticleRepository ArticleRepository =>
        LazyServiceProvider.LazyGetRequiredService<IArticleRepository>();

    public async Task<Article> CreateArticleAsync(Article article)
    {
        return await ArticleRepository.InsertAsync(article);
    }

    public void UpdateArticleContentAsync(Article article, string content)
    {
        article.UpdateContent(content);
    }
}