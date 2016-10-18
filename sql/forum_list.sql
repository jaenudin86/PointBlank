-- phpMyAdmin SQL Dump
-- version 4.2.12deb2+deb8u2
-- http://www.phpmyadmin.net
--
-- Хост: localhost
-- Время создания: Окт 18 2016 г., 18:08
-- Версия сервера: 10.1.18-MariaDB-1~jessie
-- Версия PHP: 5.6.26-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `user64765_pb`
--

-- --------------------------------------------------------

--
-- Структура таблицы `forum_list`
--

CREATE TABLE IF NOT EXISTS `forum_list` (
  `id` int(20) NOT NULL,
  `url` text NOT NULL,
  `title` text NOT NULL,
  `info` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `forum_list`
--

INSERT INTO `forum_list` (`id`, `url`, `title`, `info`) VALUES
(1, 'updates', 'Новости и обновления', 'Сводка последних новостей и обновлений на сервере'),
(2, 'tournaments', 'Турниры', 'Вся информация о проходящих турнирах'),
(3, 'community', 'Сообщество игроков', 'Свободное общение между игроками'),
(4, 'technical', 'Технический раздел', 'Вопросы, жалобы, проблемы и их решение'),
(5, 'weapons', 'Под прицелом', 'Дискуссии на тему вооружения, гайдов, тактики и стратегии'),
(6, 'testers', 'Тестирование', 'Вся информация о проходящих тестированиях'),
(7, 'suggestions', 'Предложения и пожелания', 'Ваши предложения по улучшению сервера');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
