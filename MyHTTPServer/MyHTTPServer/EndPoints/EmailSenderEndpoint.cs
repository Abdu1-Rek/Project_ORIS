using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyHttpServer.Attributes;
using MyHttpServer.Core;
using MyHttpServer.Services;

namespace MyHttpServer.EndPoints
{
	internal class EmailSenderEndpoint
	{
		[Post("/lol")]
		public void SendMailToLOL()
		{
			
		}
		[Post("/home-work")]
		public void SendMailToHomeWork()
		{
			
		}
		[Post("/bu")]
		public void SendMailToBu()
		{
			
		}
		[Post("/requests")]
		public void SendMailToRequest()
		{
			
		}
		[Post("/login")]
		public void SendMailToLogin()
		{
			
		}
		[Get("/lol")]
		public void GetLOLPage(HttpRequestContext context, string login, string password)
		{
			 Console.WriteLine("УРА!!!");


			// ------------------------------------------------
			// Надо в каждый метод подставлять данный код ниже
			var response = context.Response;
			// отправляемый в ответ код htmlвозвращает
			string responseText = "УРА Ты лол!!!";
			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			// получаем поток ответа и пишем в него ответ
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;
			// отправляем данные
			output.Write(buffer);
			output.Flush();
			// ------------------------------------------------
		}
		[Get("/home-work")]
		public void GetHomeWorkPage()
		{
			// ------------------------------------------------
			// Надо в каждый метод подставлять данный код ниже
			var response = context.Response;
			// отправляемый в ответ код htmlвозвращает
			string responseText = "УРА Домашка!!!";
			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			// получаем поток ответа и пишем в него ответ
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;
			// отправляем данные
			output.Write(buffer);
			output.Flush();
			// ------------------------------------------------
		}
		[Get("/bu")]
		public void GetBuPage()
		{
			// ------------------------------------------------
			// Надо в каждый метод подставлять данный код ниже
			var response = context.Response;
			// отправляемый в ответ код htmlвозвращает
			string responseText = "УРА Ты лол!!!";
			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			// получаем поток ответа и пишем в него ответ
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;
			// отправляем данные
			output.Write(buffer);
			output.Flush();
			// ---
		}
		[Get("/requests")]
		public void GetRequestPage()
		{
			// ------------------------------------------------
			// Надо в каждый метод подставлять данный код ниже
			var response = context.Response;
			// отправляемый в ответ код htmlвозвращает
			string responseText = "УРА Ты лол!!!";
			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			// получаем поток ответа и пишем в него ответ
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;
			// отправляем данные
			output.Write(buffer);
			output.Flush();
			// ---
		}
		[Get("/login")]
		public void GetLoginPage()
		{
			// ------------------------------------------------
			// Надо в каждый метод подставлять данный код ниже
			var response = context.Response;
			// отправляемый в ответ код htmlвозвращает
			string responseText = "УРА Ты лол!!!";
			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			// получаем поток ответа и пишем в него ответ
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;
			// отправляем данные
			output.Write(buffer);
			output.Flush();
			// ---
		}
	}
}