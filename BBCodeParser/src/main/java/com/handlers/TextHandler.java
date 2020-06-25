package com.handlers;

/**
 * Класс для преобразования текста в html код
 * */
public class TextHandler {

    /**
     * Поле для хранения обрататываемого кода
     * Значение задается только через конструктор
     * @see TextHandler#TextHandler(StringBuilder text)
     * */
    private StringBuilder text;

    /**Конструктор - создание нового элемента*/
    public TextHandler(StringBuilder text) {
        this.text = text;
    }

    /**Основной метод, преобразуещий текст в html представление
     * @return возвращает преобразованный html код
     * */
    public StringBuilder convertTextToHtml() {

        changeTag("[b", "<strong");
        changeTag("[/b", "</strong");
        changeTag("[i", "<em");
        changeTag("[/i", "</em");
        changeTag("[url", "<a href");
        changeTag("[/url", "</a");

        text = handleUniqueCase(text);

        return this.text;
    }

    /**
     * Метод для замены определенных BBCode тегов на html теги
     * @param substitutableTag - заменяемый тег
     * @param substituteTag - заменяющий тег
     * */
    private void changeTag(String substitutableTag, String substituteTag) {

        int indexOfTag;

        /*Пока не заменим все теги...*/
        while ((indexOfTag = text.indexOf(substitutableTag)) != -1) {

            /* Замена тегов*/
            text.replace(indexOfTag, indexOfTag + substitutableTag.length(), substituteTag);

            /*Замена закрывающейся прямоугольной кавычки*/
            int indexOfClosingBracket = text.indexOf("]", indexOfTag);
            text.replace(indexOfClosingBracket, indexOfClosingBracket + 1, ">");
        }
    }

    /**
     * Метод для обработки случая, когда происходит вставка ссылки с видимым URL адресом
     * @param text - преобразуемый текс
     * @return преобразованный текст
     * */
    private StringBuilder handleUniqueCase(StringBuilder text) {

        /*Пока не проверили все сылочные теги href...*/
        int indexOfTag = 0;
        while ((indexOfTag = text.indexOf("<a href", indexOfTag)) != -1) {

            /*Если есть видимый URL адрес*/
            if (isUniqueCase(text, indexOfTag)) {

                /*Получаем текст между открывающим и закрывающим тегами href*/
                int indexOfClosingBracket = text.indexOf(">", indexOfTag);
                int indexOfClosingTag = text.indexOf("</a", indexOfTag);
                String link = text.substring(indexOfClosingBracket + 1, indexOfClosingTag);

                /*Удаляем все лишние теги для получения чистой url сылки
                и присваиваем ее к атрибуту href*/
                link = deleteAllTags(link);
                text.insert(indexOfTag + 7, " = \"" + link + "\"");
            }
            /*Увеличение значения индекса тега происходит, чтобы мы не зациклились на одном теге,
             * нам ведь нужно проверить следующий тег href*/
            indexOfTag += 1;
        }
        return text;
    }

    /**
     * Метод, проверяющий есть ли в тексте видимый URL адрес
     * @param text - проверяемый текст
     * @param indexOfTag - позиция, откуда начинается проверка
     * @return true, если в тексте есть видимый URL адрес; false - если, нет
     * */
    private boolean isUniqueCase(StringBuilder text, int indexOfTag) {

        /*Если в тексте есть знак равно, то возможно значение атрибута href уже задан*/
        int indexOfEqualSign = text.indexOf("=", indexOfTag);
        if (indexOfEqualSign != -1) {

            /*Если между тегом и знаком равенства одни пробелы или и вовсе ничего нет,
            * то значит атрибут href уже задан*/
            String textAmongTagAndEqualSign = text.substring(indexOfTag + 7, indexOfEqualSign).trim();
            if (textAmongTagAndEqualSign.equals(""))
                return false;
            else
                return true;
        } else
            return true;
    }

    /**
     * Метод для удаления всех тегов в заданном тексте
     * @param s - заданный текст
     * @return чистый текст, без тегов
     * */
    private static String deleteAllTags(String s) {

        StringBuilder builder = new StringBuilder(s);
        int indexOfOpeningBracket;
        while ((indexOfOpeningBracket = builder.indexOf("<")) != -1) {

            int indexOfClosingBracket = builder.indexOf(">");
            builder.replace(indexOfOpeningBracket, indexOfClosingBracket + 1, "");
        }
        return builder.toString();
    }
}
