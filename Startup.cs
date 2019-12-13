using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ToBuy {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        readonly string PremissaoEntreOrigens = "_PermissaoEntreOrigens";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllersWithViews ().AddNewtonsoftJson (opt => {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer (options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true, // Habilita a valida��o do Issuer do token
                ValidateAudience = true, // Habilita a valida��o do Audience do token
                ValidateLifetime = true, // Habilita a valida��o do Tempo de expira��o do token
                ValidateIssuerSigningKey = true, // Permite que a Microsoft valide o token
                ValidIssuer = Configuration["Jwt:Issuer"], // Define um valor v�lido para Issuer
                ValidAudience = Configuration["Jwt:Issuer"], // Define um valor v�lido para o Audience
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["Jwt:Key"])) // Issuer Signing Key ser� igual resultado de SymetricSecurityKey recebendo a key criptografada
                };
            });

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                // Mostrar o caminho dos coment�rios dos m�todos Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });

            services.AddCors (options => {
                options.AddPolicy (PremissaoEntreOrigens,
                    builder => builder.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseCors (option =>
                option.AllowAnyOrigin ().AllowAnyMethod ().AllowAnyHeader ()
            );

            app.UseAuthentication ();

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            // Habilitamos efetivamente o Swagger em nossa aplica��o.
            app.UseSwagger ();

            // Especificamos o endpoint da documenta��o
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "API V1");
            });
            //Rodar a aplica��o e testar em: https://localhost:5001/swagger/
        }
    }
}