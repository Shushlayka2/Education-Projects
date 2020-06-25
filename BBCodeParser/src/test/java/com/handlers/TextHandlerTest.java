package com.handlers;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.HashMap;
import java.util.Map;

/**Unit - тест преобразователя текста*/
public class TextHandlerTest {

    /**
     * Map, хранящий по ключу вводимые тестовые текста
     * и по значению - ожидаемый результат
     * Заполняется в методе setTestUnits
     * @see TextHandlerTest#setTestUnits()
     * */
    Map<String, String> testUnits = new HashMap<>();

    /**Метод заполняющий Map testUnits тестовыми примерами*/
    @Before
    public void setTestUnits() {

        testUnits.put("Более подробную информацию вы сможете найти на сайте компании [url=http://icl-services.com]ICL Services[/url] или портале [b]ICL[/b] http://www.icl.ru",
                "Более подробную информацию вы сможете найти на сайте компании <a href=http://icl-services.com>ICL Services</a> или портале <strong>ICL</strong> http://www.icl.ru");

        testUnits.put("[url]https://ru.wikipedia.org[/url]",
                "<a href = \"https://ru.wikipedia.org\">https://ru.wikipedia.org</a>");

        testUnits.put("[url rel   =   \"nofollow\"  ]https://ru.wikipedia.org[/url]",
                "<a href = \"https://ru.wikipedia.org\" rel   =   \"nofollow\"  >https://ru.wikipedia.org</a>");
    }

    /**Метод, запускающий сам тест*/
    @Test
    public void testTextHandler() {

        for (Map.Entry<String, String> entry : testUnits.entrySet()) {
            StringBuilder result = new TextHandler(new StringBuilder(entry.getKey())).convertTextToHtml();
            Assert.assertEquals(entry.getValue(), result.toString());
        }
    }
}
