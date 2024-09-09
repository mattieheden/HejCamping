namespace HejCamping.Domain.Services
{
    public interface IViewRenderer
    {
        Task<string> RenderViewToStringAsync(string viewName, object model);
    }
}