using SchoolMangment.Database;
using SchoolMangment.Dtos;
using SchoolMangment.Filters;
using SchoolMangment.Repostories;
using SchoolMangment.Utils;
using SqlSugar;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RedisConnection>(builder.Configuration.GetSection("RedisConnection"));
builder.Services.Configure<EmailDto>(builder.Configuration.GetSection("sendEmailInfo"));
builder.Services.AddSingleton(sp => new SqlSugarContext(
    new SqlSugarClient(new ConnectionConfig()
    {
        ConnectionString = builder.Configuration.GetConnectionString("DbConnectionString"), //数据库连接串
        DbType = DbType.MySql,      //数据库类型
        IsAutoCloseConnection = true//自动释放
    })
));
builder.Services.AddSingleton<ICache, RedisCacheHelper>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassesRepository, ClassesRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<MyExceptionFilter>();
    options.Filters.Add<MyActionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<AbstractSendEmailHelper, QQSendEmailHelper>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
