package com.servlets;

import com.handlers.TextHandler;
import org.apache.log4j.Logger;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

/**Сервлет для обработки запросов, полученных от веб-формы*/
public class MainServlet extends HttpServlet{

    /**Поле - логер*/
    private final static Logger logger = Logger.getLogger(MainServlet.class);

    /**
     * Метод для обработки post запросов
     * @exception IOException - ошибка при получении объекта для вывода результата в виде html документа
     * */
    public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {

        /*Получаем текст на языке разметки BBCode*/
        StringBuilder text = new StringBuilder(request.getParameter("BBCodeText"));
        logger.info("Received text in markup language BBCode");

        /*Преобразуем полученный код в html код*/
        TextHandler handler = new TextHandler(text);
        text = handler.convertTextToHtml();
        logger.info("Text is converted to html code");

        /*Отображение html кода на странице*/
        response.setContentType("text/html;charset=utf-8");
        response.setStatus(HttpServletResponse.SC_OK);
        PrintWriter writer = response.getWriter();
        writer.append(text);
        logger.info("Converted text is displayed on the page");
    }
}
