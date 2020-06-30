using System;
using System.IO;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PuppeteerSharpDotNetCodeInDockerDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var downloadsDirectory = new DirectoryInfo("/home/pptruser/Downloads");
            var fileName = $@"{DateTime.Now:yyyyMMddHHmmss}.jpg";
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions()
            {
                Headless = true
            });
            await using var page = await browser.NewPageAsync();
            await page.GoToAsync("https://www.hardkoded.com");
            Console.WriteLine(
                await page
                    .QuerySelectorAsync(".page-subheading")
                    .EvaluateFunctionAsync<string>("el => el.innerText"));
            await page.GoToAsync("https://1968.freeway.gov.tw/");
            //透過SetViewport控制視窗大小決定抓圖尺寸
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1024,
                Height = 768
            });
            foreach (var region in new[] { "N", "C", "P" })
            {
                // 呼叫網頁程式方法切換區域
                await page.EvaluateExpressionAsync($"region('{region}')");
                // 要等待網頁切換顯示完成再抓圖
                await page.WaitForSelectorAsync("div.fwoverlay");
                // 抓網頁畫面存檔
                await page.ScreenshotAsync(Path.Combine(downloadsDirectory.FullName, fileName));
            }
        }
    }
}
