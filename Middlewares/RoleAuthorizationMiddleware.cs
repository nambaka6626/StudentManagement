using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StudentManagement.Middlewares
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Xử lý kiểm tra quyền truy cập tại đây (tùy chỉnh nếu cần)
            await _next(context);
        }
    }
}
