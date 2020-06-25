package com.server;

import com.servlets.MainServlet;
import org.apache.log4j.Logger;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.server.handler.HandlerList;
import org.eclipse.jetty.server.handler.ResourceHandler;
import org.eclipse.jetty.servlet.ServletContextHandler;
import org.eclipse.jetty.servlet.ServletHolder;

/**
 * Основной класс для запуска сервера
 * @author Latypov Bulat
 * @version 1.0
 */
public class ServerRunner {

    /**Поле - логер*/
    private final static Logger logger = Logger.getLogger(ServerRunner.class);

    /**Поле - сервер*/
    private static Server server = new Server(8080);

    /**Основной метод, запускающий сервер*/
    public static void main(String[] args) {

        ServletContextHandler context = new ServletContextHandler(ServletContextHandler.SESSIONS);
        context.addServlet(new ServletHolder(new MainServlet()), "/htmlParsedCode");

        ResourceHandler resourceHandler = new ResourceHandler();
        resourceHandler.setResourceBase("src/main/java/com/htmls/WebForm.html");

        HandlerList handlerList = new HandlerList();
        handlerList.addHandler(resourceHandler);
        handlerList.addHandler(context);

        try {
            server.setHandler(handlerList);
            server.start();
            logger.info("Server started");
            server.join();
        } catch (Exception e) {
            e.printStackTrace();
            logger.error("Server exception!/n" + e.getMessage());
        }
    }
}
