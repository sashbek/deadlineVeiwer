using System;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace deadlineViewer.Parser
{
    class HtmlLoader
    {
        readonly IWebDriver driver;
        readonly string url;
        public HtmlLoader(IParserSettings settings)
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(@"./Resources/chromedriver.exe");
            chromeDriverService.HideCommandPromptWindow = true;
            var option = new ChromeOptions();
            //off showing browser
            option.AddArguments(
                "--headless", "--no-sandbox",
                "--disable-web-security", "--disable-gpu",
                "--incognito", "--proxy-bypass-list=*",
                "--proxy-server='direct://'", "--log-level=OFF",
                "--hide-scrollbars");

            driver = new ChromeDriver(chromeDriverService, option);
            driver.Url = settings.BaseUrl;
            url = settings.BaseUrl;
        }


        public async Task<string> GetSource()
        {
            strt:
                try
                {
                    driver.FindElement(By.ClassName("notion-table-view-cell"));
                }
                catch (Exception e)
                {
                    goto strt;
                }

            string source = driver.FindElement(By.ClassName("notion-html")).GetAttribute("innerHTML");

            driver.Quit();

            return source;
        }
    }
}
