package com.handlers;

import com.server.ServerRunner;
import org.eclipse.jetty.server.Server;
import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.ie.InternetExplorerDriver;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.lang.reflect.Field;
import java.util.concurrent.TimeUnit;

/**Класс - Интеграционный тест*/
public class IntegrationTest {

    /**Поле - поток для чтения лог файла*/
    private static Thread listenerThread;

    /**Поле - web драйвер*/
    private static WebDriver driver;

    /**Настройка web драйвера*/
    @Before
    public void driverSetting(){

        System.setProperty("webdriver.ie.driver", "IEDriverServer.exe");
        driver = new InternetExplorerDriver();
        driver.manage().timeouts().implicitlyWait(50, TimeUnit.SECONDS);
    }

    /**Метод для запуска потока чтения логов*/
    @Before
    public void startListener() {

        listenerThread = new Thread(new Listener());
        listenerThread.start();
    }

    /**Метод, запускающий сам тест*/
    @Test
    public void startTesting() {
        try {
            runServer();
            Thread.sleep(1000);
            Assert.assertEquals("Server started", Listener.getLastLog());
            openWebSite();
            sendBBCode();
            closeWebSite();
            stopServer();
            Thread.sleep(1000);
            Assert.assertEquals("Converted text is displayed on the page", Listener.getLastLog());
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /**Метод для остановки потока чтения логов*/
    @After
    public void stopListener() {
        listenerThread.interrupt();
    }

    /*---------------------------------------------details----------------------------------------------------------*/

    /**Класс обработки логов*/
    private static class Listener implements Runnable {

        /**Поле для хранения текущего потока*/
        private Thread current = Thread.currentThread();

        /**BufferedReader для чтения из файла логов*/
        private static BufferedReader reader;

        /**Поле для хранения последнего лога*/
        private static String lastLog;

        /**Статический блог, задающий BufferedReader, читающий с определенного файла*/
        static {
            try {
                reader = new BufferedReader(new FileReader("src/main/java/log/application.log"));
            } catch (FileNotFoundException e) {
                e.printStackTrace();
            }
        }

        /**
         * Метод для получения значения поля {@link Listener#lastLog}
         * @return возвращает последний лог
         * */
        public static String getLastLog() {
            return lastLog;
        }

        /**
         * Метод, исполняемый в параллельном потоке, обновляет последний лог {@link Listener#lastLog}
         * при появлении нового в лог файле
         * */
        @Override
        public void run() {
            while (!current.isInterrupted()) {
                try {
                    String line = reader.readLine();
                    if ( line!= null)
                        lastLog = line;
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    /**Метод, запускающий сервер в параллельном потоке*/
    private void runServer() {
        new Thread(() -> {
            ServerRunner.main(null);
        }).start();
    }

    /**Метод, открывающий веб-страницу*/
    private void openWebSite() {
        driver.get("http://localhost:8080");
    }

    /**Метод для автоматического ввода кода в веб-форму*/
    private void sendBBCode() {
        WebElement textarea = driver.findElement(By.name("BBCodeText"));
        textarea.sendKeys("[url]https://ru.[b]wikipedia[/b].org[/url]");
        WebElement sendButton = driver.findElement(By.name("send"));
        sendButton.click();
    }

    /**Метод, останавливающий работу сервера*/
    private void stopServer() {
        try {
            Field field = ServerRunner.class.getDeclaredField("server");
            field.setAccessible(true);
            Server server = (Server) field.get(null);
            server.stop();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    /**Метод, закрывающий браузер*/
    private void closeWebSite() {
        driver.quit();
    }
}
