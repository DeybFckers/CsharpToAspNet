using LearnMiddleware.MIddleware;
using System;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware runs in order of registration (pipeline),
// but execution flows forward and backward using next()

//Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    /* add this if you want to modify a network headers
     * context.Response.Headers["MyHeader"] = "My header context"; */

    await context.Response.WriteAsync("Middleware #1: Before Calling next\r\n");

    await next(context);
    
    await context.Response.WriteAsync("Middleware #1: After Calling next\r\n");
});

//when you want a true or false middleware you can use app.MapWhen and change it to this 
app.MapWhen((context) =>
    context.Request.Query.ContainsKey("id"), /*when true it means if the employees has id then it will execute the appBuilder 
    but the consequence is it will still run even no path as long as you have the id */

    /* you can create your own logic, you can do this
     * return context.Request.Path.StartsWithSegments("/employees") &&
     * context.Request.Query.ContainsKey("id"),  */

    (appBuilder) =>
    {
        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Middleware #4: Before Calling next\r\n");

            await next(context);

            await context.Response.WriteAsync("Middleware #4: After Calling next\r\n");
        });

        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("Middleware #5: Before Calling next\r\n");

            await next(context);

            await context.Response.WriteAsync("Middleware #5: After Calling next\r\n");
        });
    }
);
//when false it will run the original middleware or the 4 and 5 wont run

/*you can also app.UseWhen if you want a true or false middleware
 * but the difference of MapWhen and UseWhen, the UseWhen will continue 
 * to other middleware after it executed which is 2 and 3 and the MapWhen
 * it wont go to other middleware, so instead of going to 2 and 3 it will 
 * go back to middleware 1 */ 
     


///* when you create a multiple branch you can use app.Map */
//app.Map("/employees", (appBuilder) =>
//{

//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #4: Before Calling next\r\n");

//        await next(context);

//        await context.Response.WriteAsync("Middleware #4: After Calling next\r\n");
//    });

//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #5: Before Calling next\r\n");

//        await next(context);

//        await context.Response.WriteAsync("Middleware #5: After Calling next\r\n");
//    });

//});
///* using app.Map it means it will skip the other middleware so instead
// * of going to middleware 2 and 3 it will just return to middleware 1
// * after it execute the app.Map */


/* you can create this way if you want to have shortcircuit
 * instead of putting (HttpContext and RequestDelgate)
app.Use(async (context, next) =>
{           
    await context.Response.WriteAsync("Middleware #2: Before Calling next\r\n");

    await context.Response.WriteAsync("Middleware #3: After Calling next\r\n");
});
 */


//Middleware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #2: Before Calling next\r\n");

    //this mean it will go to next middleware
    await next(context);
    /*you can remove this if you want to have short circuit pipeline
    short circuit means we dont to a next middleware anymore,
    so instead of going to a middleware 3 it will just return
    */

    await context.Response.WriteAsync("Middleware #2: After Calling next\r\n");
});


///*you can also use app.Run to create middleware for component*/
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware #2: Processed\r\n");
//});
////using an app.run means you want to stop or the other middleware will not be executed


//Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before Calling next\r\n");

    await next(context);

    await context.Response.WriteAsync("Middleware #3: After Calling next\r\n");
});


//to register the class as service do this
builder.Services.AddTransient<MyCustomMiddleware>();


app.Run();
