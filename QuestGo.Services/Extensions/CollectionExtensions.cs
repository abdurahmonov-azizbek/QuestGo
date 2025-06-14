using Newtonsoft.Json;
using QuestGo.Domain.Commons;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Helpers;

namespace QuestGo.Services.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> entities, PaginationParams @params)
        where TEntity : Auditable
    {
        var metaData = new PaginationMetaData(entities.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
            HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

#pragma warning disable ASP0019
        HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
#pragma warning restore ASP0019

        return @params is { PageIndex: > 0, PageSize: > 0 } ?
            entities.OrderBy(e => e.Id)
                .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
            throw new QuestGoException(400, "Please, enter valid numbers");
    }
}
