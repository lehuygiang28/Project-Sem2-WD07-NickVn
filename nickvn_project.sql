-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Oct 26, 2022 at 05:06 PM
-- Server version: 10.4.25-MariaDB
-- PHP Version: 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `nickvn_project`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `category_id` int(11) NOT NULL,
  `name` text NOT NULL,
  `title` text NOT NULL,
  `action` text NOT NULL DEFAULT '#',
  `img_sale_off` text NOT NULL,
  `img_src` text NOT NULL,
  `total` int(11) NOT NULL DEFAULT 0,
  `note` text NOT NULL,
  `status` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`category_id`, `name`, `title`, `action`, `img_sale_off`, `img_src`, `total`, `note`, `status`) VALUES
(1, 'Liên Minh', 'Danh mục game liên minh', 'Garena', '/storage/images/sale-off-30.png', '/storage/images/lien-minh.gif', 12335, '', 1),
(2, 'Liên Quân', 'Danh mục game Liên Quân', '#', '/storage/images/sale-off-30.png', '/storage/images/lien-quan.gif', 24235, '', 1),
(3, 'Ngọc Rồng', 'Danh mục game Ngọc Rồng', '#', '/storage/images/sale-off-30.png', '/storage/images/ngoc-rong.gif', 11241, '', 1);

-- --------------------------------------------------------

--
-- Table structure for table `charge_history`
--

CREATE TABLE `charge_history` (
  `phone_card_history_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `telecom` text NOT NULL,
  `pin` text NOT NULL,
  `serial` text NOT NULL,
  `type_charge` text NOT NULL,
  `amount` decimal(11,0) NOT NULL,
  `money_received` decimal(11,0) NOT NULL,
  `note` text DEFAULT NULL,
  `result` text NOT NULL,
  `create_at` datetime NOT NULL,
  `update_at` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `charge_history`
--

INSERT INTO `charge_history` (`phone_card_history_id`, `user_id`, `telecom`, `pin`, `serial`, `type_charge`, `amount`, `money_received`, `note`, `result`, `create_at`, `update_at`) VALUES
(1, 1, 'VIETTEL', '1234567891234', '1234567891234', 'Nạp thẻ cào', '100000', '100000', NULL, 'Thành công', '2022-10-06 22:26:01', '2022-10-06 22:26:01'),
(2, 1, 'VIETTEL', '6841495941842 ', '1961559454096 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-08 00:18:46', '2022-10-08 00:18:46'),
(3, 1, 'VIETTEL', '6841495941842 ', '1961559454096 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-08 00:21:17', '2022-10-08 00:21:17'),
(4, 1, 'VCOIN', '9805304611996 ', '9480324646330 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-08 00:25:23', '2022-10-08 00:25:23'),
(5, 1, 'VCOIN', '9805304611996 ', '9480324646330 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-08 00:27:51', '2022-10-08 00:27:51'),
(6, 1, 'VCOIN', '9805304611996 ', '9480324646330 ', 'Nạp thẻ cào', '500000', '200000', 'Sai mệnh giá, mệnh giá gốc 500000, mệnh giá chọn 30000, nhận được 200000', 'Thành công, sai mệnh giá', '2022-10-08 00:28:09', '2022-10-08 00:28:09'),
(7, 1, 'VCOIN', '9805304611996 ', '9480324646330 ', 'Nạp thẻ cào', '500000', '200000', 'Sai mệnh giá, mệnh giá gốc 500000, mệnh giá chọn 200000, nhận được 200000', 'Thành công, sai mệnh giá', '2022-10-08 00:33:36', '2022-10-08 00:33:36'),
(8, 1, 'VIETNAMMOBILE', '4437940485559 ', '7316553379805 ', 'Nạp thẻ cào', '300000', '300000', NULL, 'Thành công', '2022-10-08 00:34:25', '2022-10-08 00:34:25'),
(9, 1, 'ZING', '0441221936349 ', '1681465840016 ', 'Nạp thẻ cào', '300000', '300000', NULL, 'Thành công', '2022-10-08 01:05:21', '2022-10-08 01:05:21'),
(10, 3, 'VIETTEL', '5569098255267 ', '7354868263367 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-09 01:18:33', '2022-10-09 01:18:33'),
(11, 3, 'VIETNAMMOBILE', '8228288069091 ', '4163142102747 ', 'Nạp thẻ cào', '300000', '120000', 'Sai mệnh giá, mệnh giá gốc 300000, mệnh giá chọn 500000, nhận được 120000', 'Thành công, sai mệnh giá', '2022-10-09 01:19:31', '2022-10-09 01:19:31'),
(12, 3, 'VCOIN', '2647835345757 ', '7042887604165 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-11 14:26:55', '2022-10-11 14:26:55'),
(13, 3, 'VIETTEL', '7622549781694 ', '1010903249383 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-11 14:27:30', '2022-10-11 14:27:30'),
(14, 2, 'VIETTEL', '9784181116572 ', '8324398184925 ', 'Nạp thẻ cào', '500000', '200000', 'Sai mệnh giá, mệnh giá gốc 500000, mệnh giá chọn 300000, nhận được 200000', 'Thành công, sai mệnh giá', '2022-10-11 14:31:04', '2022-10-11 14:31:04'),
(15, 2, 'GATE', '1845932499459 ', '7109667170830 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-11 14:31:51', '2022-10-11 14:31:51'),
(16, 1, 'VIETTEL', '9109873101140 ', '8484238505353 ', 'Nạp thẻ cào', '500000', '500000', NULL, 'Thành công', '2022-10-22 17:18:12', '2022-10-22 17:18:12');

-- --------------------------------------------------------

--
-- Table structure for table `googlerecaptcha`
--

CREATE TABLE `googlerecaptcha` (
  `id` int(11) NOT NULL,
  `host_name` text DEFAULT NULL,
  `site_key` text NOT NULL,
  `secret_key` text NOT NULL,
  `Response` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `googlerecaptcha`
--

INSERT INTO `googlerecaptcha` (`id`, `host_name`, `site_key`, `secret_key`, `Response`) VALUES
(1, 'GoogleReCaptcha', '6Lc55R4fAAAAAEh4qaPNR6fVcwu44zSkIHKrVMng', '6Lc55R4fAAAAALGca6ts3hikUsFbjHT74ytvHqoR', NULL),
(2, 'admin.giang.cf', '6Lc55R4fAAAAAEh4qaPNR6fVcwu44zSkIHKrVMng', '6Lc55R4fAAAAALGca6ts3hikUsFbjHT74ytvHqoR', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `images`
--

CREATE TABLE `images` (
  `img_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `img_link` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `images`
--

INSERT INTO `images` (`img_id`, `product_id`, `img_link`) VALUES
(2, 2, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(3, 2, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(4, 2, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(5, 2, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(6, 2, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(7, 2, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(8, 3, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(9, 3, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(10, 3, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(11, 3, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(12, 3, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(13, 3, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(14, 4, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(15, 4, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(16, 4, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(17, 4, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(18, 4, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(19, 4, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(20, 5, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(21, 5, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(22, 5, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(23, 5, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(24, 5, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(25, 5, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(26, 6, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(27, 6, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(28, 6, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(29, 6, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(30, 6, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(31, 6, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(32, 7, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(33, 7, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(34, 7, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(35, 7, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(36, 7, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(37, 7, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(38, 8, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(39, 8, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(40, 8, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(41, 8, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(42, 8, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(43, 8, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(44, 9, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(45, 9, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(46, 9, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(47, 9, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(48, 9, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(49, 9, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(50, 10, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(51, 10, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(52, 10, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(53, 10, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(54, 10, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(55, 10, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(56, 11, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(57, 11, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(58, 11, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(59, 11, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(60, 11, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(61, 11, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(62, 12, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(63, 12, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(64, 12, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(65, 12, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(66, 12, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(67, 12, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(68, 13, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(69, 13, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(70, 13, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(71, 13, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(72, 13, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(73, 13, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(74, 14, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(75, 14, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(76, 14, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(77, 14, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(78, 14, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(79, 14, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(80, 15, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(81, 15, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(82, 15, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(83, 15, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(84, 15, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(85, 15, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(86, 16, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(87, 16, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(88, 16, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(89, 16, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(90, 16, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(91, 16, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(92, 17, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(93, 17, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(94, 17, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(95, 17, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(96, 17, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(97, 17, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(98, 18, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(99, 18, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(100, 18, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(101, 18, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(102, 18, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(103, 18, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(104, 19, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(105, 19, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(106, 19, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(107, 19, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(108, 19, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(109, 19, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(110, 20, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(111, 20, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(112, 20, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(113, 20, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(114, 20, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(115, 20, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(116, 21, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(117, 21, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(118, 21, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(119, 21, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(120, 21, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(121, 21, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(122, 22, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(123, 22, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(124, 22, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(125, 22, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(126, 22, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(127, 22, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(128, 23, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(129, 23, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(130, 23, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(131, 23, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(132, 23, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(133, 23, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(134, 24, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(135, 24, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(136, 24, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(137, 24, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(138, 24, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(139, 24, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(140, 25, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(141, 25, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(142, 25, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(143, 25, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(144, 25, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(145, 25, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(146, 26, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(147, 26, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(148, 26, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(149, 26, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(150, 26, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(151, 26, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(152, 27, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(153, 27, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(154, 27, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(155, 27, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(156, 27, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(157, 27, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(158, 28, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(159, 28, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(160, 28, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(161, 28, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(162, 28, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(163, 28, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(164, 29, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(165, 29, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(166, 29, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(167, 29, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(168, 29, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(169, 29, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(170, 30, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(171, 30, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(172, 30, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(173, 30, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(174, 30, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(175, 30, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(176, 31, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(177, 31, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(178, 31, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(179, 31, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(180, 31, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(181, 31, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(182, 32, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(183, 32, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(184, 32, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(185, 32, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(186, 32, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(187, 32, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(188, 33, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(189, 33, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(190, 33, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(191, 33, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(192, 33, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(193, 33, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(194, 34, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(195, 34, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(196, 34, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(197, 34, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(198, 34, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(199, 34, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(200, 35, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(201, 35, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(202, 35, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(203, 35, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(204, 35, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(205, 35, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(206, 36, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(207, 36, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(208, 36, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(209, 36, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(210, 36, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(211, 36, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(212, 37, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(213, 37, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(214, 37, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(215, 37, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(216, 37, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(217, 37, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(218, 38, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(219, 38, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(220, 38, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(221, 38, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(222, 38, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(223, 38, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(224, 39, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(225, 39, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(226, 39, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(227, 39, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(228, 39, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(229, 39, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(230, 40, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(231, 40, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(232, 40, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(233, 40, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(234, 40, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(235, 40, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(236, 41, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(237, 41, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(238, 41, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(239, 41, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(240, 41, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(241, 41, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(242, 42, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(243, 42, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(244, 42, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(245, 42, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(246, 42, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(247, 42, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(248, 43, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(249, 43, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(250, 43, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(251, 43, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(252, 43, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(253, 43, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(254, 44, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(255, 44, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(256, 44, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(257, 44, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(258, 44, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(259, 44, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(260, 45, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(261, 45, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(262, 45, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(263, 45, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(264, 45, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(265, 45, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(266, 46, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(267, 46, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(268, 46, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(269, 46, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(270, 46, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(271, 46, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(272, 47, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(273, 47, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(274, 47, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(275, 47, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(276, 47, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(277, 47, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(278, 48, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(279, 48, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(280, 48, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(281, 48, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(282, 48, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(283, 48, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(284, 49, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(285, 49, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(286, 49, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(287, 49, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(288, 49, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(289, 49, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(290, 50, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(291, 50, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(292, 50, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(293, 50, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(294, 50, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(295, 50, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(296, 51, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(297, 51, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(298, 51, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(299, 51, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(300, 51, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(301, 51, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(302, 52, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(303, 52, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(304, 52, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(305, 52, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(306, 52, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(307, 52, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(308, 53, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(309, 53, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(310, 53, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(311, 53, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(312, 53, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(313, 53, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(314, 54, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(315, 54, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(316, 54, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(317, 54, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(318, 54, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(319, 54, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(320, 55, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(321, 55, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(322, 55, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(323, 55, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(324, 55, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(325, 55, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(326, 56, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(327, 56, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(328, 56, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(329, 56, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(330, 56, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(331, 56, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(332, 57, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(333, 57, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(334, 57, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(335, 57, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(336, 57, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(337, 57, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(338, 58, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(339, 58, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(340, 58, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(341, 58, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(342, 58, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(343, 58, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(344, 59, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(345, 59, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(346, 59, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(347, 59, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(348, 59, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(349, 59, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(350, 60, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(351, 60, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(352, 60, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(353, 60, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(354, 60, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(355, 60, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(356, 61, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(357, 61, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(358, 61, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(359, 61, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(360, 61, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(361, 61, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(362, 62, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(363, 62, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(364, 62, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(365, 62, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(366, 62, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(367, 62, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(368, 63, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(369, 63, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(370, 63, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(371, 63, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(372, 63, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(373, 63, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(374, 64, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(375, 64, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(376, 64, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(377, 64, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(378, 64, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(379, 64, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(380, 65, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(381, 65, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(382, 65, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(383, 65, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(384, 65, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(385, 65, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(386, 66, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(387, 66, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(388, 66, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(389, 66, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(390, 66, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(391, 66, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(392, 67, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(393, 67, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(394, 67, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(395, 67, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(396, 67, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(397, 67, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(398, 68, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(399, 68, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(400, 68, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(401, 68, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(402, 68, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(403, 68, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(404, 69, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(405, 69, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(406, 69, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(407, 69, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(408, 69, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(409, 69, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(410, 70, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(411, 70, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(412, 70, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(413, 70, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(414, 70, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(415, 70, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(416, 71, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(417, 71, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(418, 71, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(419, 71, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(420, 71, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(421, 71, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(422, 72, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(423, 72, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(424, 72, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(425, 72, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(426, 72, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(427, 72, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(428, 73, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(429, 73, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(430, 73, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(431, 73, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(432, 73, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(433, 73, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(434, 74, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(435, 74, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(436, 74, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(437, 74, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(438, 74, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(439, 74, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(440, 75, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(441, 75, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(442, 75, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(443, 75, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(444, 75, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(445, 75, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(446, 76, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(447, 76, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(448, 76, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(449, 76, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(450, 76, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(451, 76, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(452, 77, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(453, 77, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(454, 77, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(455, 77, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(456, 77, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(457, 77, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(458, 78, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(459, 78, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(460, 78, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(461, 78, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(462, 78, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(463, 78, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(464, 79, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(465, 79, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(466, 79, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(467, 79, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(468, 79, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(469, 79, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(470, 80, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(471, 80, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(472, 80, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(473, 80, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(474, 80, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(475, 80, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(476, 81, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(477, 81, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(478, 81, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(479, 81, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(480, 81, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(481, 81, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(482, 82, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(483, 82, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(484, 82, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(485, 82, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(486, 82, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(487, 82, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(488, 83, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(489, 83, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(490, 83, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(491, 83, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(492, 83, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(493, 83, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(494, 84, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(495, 84, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(496, 84, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(497, 84, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(498, 84, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(499, 84, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(500, 85, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(501, 85, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(502, 85, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(503, 85, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(504, 85, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(505, 85, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(506, 86, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(507, 86, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(508, 86, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(509, 86, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(510, 86, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(511, 86, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(512, 87, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(513, 87, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(514, 87, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(515, 87, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(516, 87, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(517, 87, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(518, 88, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(519, 88, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(520, 88, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(521, 88, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(522, 88, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(523, 88, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(524, 89, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(525, 89, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(526, 89, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(527, 89, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(528, 89, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(529, 89, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(530, 90, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(531, 90, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(532, 90, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(533, 90, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(534, 90, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(535, 90, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(536, 91, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(537, 91, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(538, 91, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(539, 91, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(540, 91, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(541, 91, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(542, 92, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(543, 92, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(544, 92, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(545, 92, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(546, 92, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(547, 92, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(548, 93, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(549, 93, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(550, 93, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(551, 93, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(552, 93, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(553, 93, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(554, 94, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(555, 94, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(556, 94, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(557, 94, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(558, 94, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(559, 94, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(560, 95, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(561, 95, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(562, 95, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(563, 95, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(564, 95, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(565, 95, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(566, 96, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(567, 96, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(568, 96, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(569, 96, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(570, 96, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(571, 96, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(572, 97, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(573, 97, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(574, 97, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(575, 97, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(576, 97, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(577, 97, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(578, 98, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(579, 98, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(580, 98, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(581, 98, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(582, 98, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(583, 98, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(584, 99, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(585, 99, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(586, 99, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(587, 99, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(588, 99, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(589, 99, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(590, 100, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(591, 100, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(592, 100, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(593, 100, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(594, 100, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(595, 100, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(596, 101, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(597, 101, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(598, 101, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(599, 101, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(600, 101, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(601, 101, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(602, 102, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(603, 102, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(604, 102, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(605, 102, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(606, 102, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(607, 102, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(608, 3456, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(609, 3456, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(610, 3456, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(611, 3456, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(612, 3456, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(613, 3456, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(614, 3457, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(615, 3457, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(616, 3457, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(617, 3457, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(618, 3457, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(619, 3457, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(620, 3458, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(621, 3458, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(622, 3458, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(623, 3458, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(624, 3458, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(625, 3458, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(626, 3459, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(627, 3459, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(628, 3459, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(629, 3459, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(630, 3459, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(631, 3459, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(632, 3460, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(633, 3460, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(634, 3460, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(635, 3460, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(636, 3460, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(637, 3460, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(638, 3461, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(639, 3461, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(640, 3461, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(641, 3461, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(642, 3461, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(643, 3461, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(644, 3462, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(645, 3462, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(646, 3462, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(647, 3462, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(648, 3462, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(649, 3462, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(650, 3463, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(651, 3463, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(652, 3463, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(653, 3463, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(654, 3463, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(655, 3463, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(656, 3464, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(657, 3464, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(658, 3464, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(659, 3464, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(660, 3464, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(661, 3464, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(662, 3465, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(663, 3465, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(664, 3465, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(665, 3465, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(666, 3465, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(667, 3465, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(668, 3466, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(669, 3466, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(670, 3466, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(671, 3466, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(672, 3466, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(673, 3466, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(674, 3467, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(675, 3467, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(676, 3467, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(677, 3467, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(678, 3467, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(679, 3467, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(680, 3468, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(681, 3468, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(682, 3468, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(683, 3468, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(684, 3468, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(685, 3468, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(686, 3469, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(687, 3469, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(688, 3469, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(689, 3469, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(690, 3469, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(691, 3469, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(692, 3470, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(693, 3470, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(694, 3470, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(695, 3470, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(696, 3470, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(697, 3470, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(698, 3471, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(699, 3471, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(700, 3471, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(701, 3471, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(702, 3471, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(703, 3471, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(704, 3472, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(705, 3472, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(706, 3472, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(707, 3472, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(708, 3472, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(709, 3472, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(710, 3473, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(711, 3473, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(712, 3473, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(713, 3473, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(714, 3473, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(715, 3473, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(716, 3474, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(717, 3474, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(718, 3474, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(719, 3474, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(720, 3474, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(721, 3474, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(722, 3475, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(723, 3475, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(724, 3475, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(725, 3475, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(726, 3475, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(727, 3475, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(728, 3476, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(729, 3476, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(730, 3476, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(731, 3476, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(732, 3476, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(733, 3476, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(734, 3477, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(735, 3477, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(736, 3477, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(737, 3477, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(738, 3477, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(739, 3477, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(740, 3478, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(741, 3478, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(742, 3478, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(743, 3478, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(744, 3478, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(745, 3478, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(746, 3479, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(747, 3479, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(748, 3479, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(749, 3479, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(750, 3479, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(751, 3479, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(752, 3480, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(753, 3480, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(754, 3480, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(755, 3480, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(756, 3480, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(757, 3480, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(758, 3481, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(759, 3481, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(760, 3481, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(761, 3481, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(762, 3481, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(763, 3481, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(764, 3482, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(765, 3482, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(766, 3482, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(767, 3482, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(768, 3482, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(769, 3482, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(770, 3483, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(771, 3483, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(772, 3483, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(773, 3483, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(774, 3483, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(775, 3483, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(776, 3484, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(777, 3484, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(778, 3484, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(779, 3484, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(780, 3484, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(781, 3484, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(782, 3485, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(783, 3485, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(784, 3485, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(785, 3485, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(786, 3485, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(787, 3485, '/storage/images/xxuC88f0h9_1632531414.jpg'),
(788, 3486, '/storage/images/0qBPw7AiOQ_1632531413.jpg'),
(789, 3486, '/storage/images/CM9q56zAnM_1632531413.jpg'),
(790, 3486, '/storage/images/pTWDgoJQuz_1632531413.jpg'),
(791, 3486, '/storage/images/zwRCsqMtyo_1632531413.jpg'),
(792, 3486, '/storage/images/HLpEr7ojZm_1632531414.jpg'),
(793, 3486, '/storage/images/xxuC88f0h9_1632531414.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `lienminh`
--

CREATE TABLE `lienminh` (
  `product_id` int(11) NOT NULL,
  `name` text NOT NULL,
  `product_category_id` int(11) NOT NULL,
  `product_user_name` text NOT NULL,
  `product_user_password` text NOT NULL,
  `publisher` text NOT NULL,
  `price_atm` decimal(10,0) NOT NULL DEFAULT 0,
  `champ` int(11) NOT NULL DEFAULT 0,
  `skin` int(11) NOT NULL DEFAULT 0,
  `rank` text NOT NULL,
  `status_account` text NOT NULL,
  `note` text DEFAULT NULL,
  `img_thumb` text NOT NULL,
  `status_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `lienminh`
--

INSERT INTO `lienminh` (`product_id`, `name`, `product_category_id`, `product_user_name`, `product_user_password`, `publisher`, `price_atm`, `champ`, `skin`, `rank`, `status_account`, `note`, `img_thumb`, `status_id`) VALUES
(2, 'Liên Minh', 1, 'shop1CIWNr', 'passwsTTCQun8', 'Garena', '13139066', 110, 15, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1005),
(3, 'Liên Minh', 1, 'shopHEJ3eh', 'passw1JTGYMNe', 'Garena', '9168005', 38, 237, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(4, 'Liên Minh', 1, 'shopwOSmdx', 'passwMt9G3zum', 'Garena', '1758758', 127, 429, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(5, 'Liên Minh', 1, 'shop7o9fW3', 'passwem4dXqU5', 'Garena', '7611599', 97, 290, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(6, 'Liên Minh', 1, 'shopy2Jbld', 'passwKJMSfP8N', 'Garena', '17080940', 48, 302, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1004),
(7, 'Liên Minh', 1, 'shop7aZdtY', 'passweeQuYLT8', 'Garena', '7866767', 41, 516, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(8, 'Liên Minh', 1, 'shop6g10N7', 'passw0BmTiHJY', 'Garena', '20572152', 76, 534, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(9, 'Liên Minh', 1, 'shop2VzQID', 'passw4P8y4EoK', 'Garena', '4527048', 33, 425, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(10, 'Liên Minh', 1, 'shopXmzGWM', 'passwYfuqKzSw', 'Garena', '599000', 37, 319, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(11, 'Liên Minh', 1, 'shoptOGfVh', 'passwp66RtJe0', 'Garena', '12989569', 131, 399, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(12, 'Liên Minh', 1, 'shopmtynCN', 'passwNLvXQymG', 'Garena', '16155031', 24, 10, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(13, 'Liên Minh', 1, 'shopoVtRYt', 'passwfW9oCfGV', 'Garena', '3787434', 112, 331, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(14, 'Liên Minh', 1, 'shopvzPR1K', 'passwrCN6n1al', 'Garena', '1230000', 5, 511, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(15, 'Liên Minh', 1, 'shopT5lWEM', 'passwt7vqJqLP', 'Garena', '5091654', 25, 487, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(16, 'Liên Minh', 1, 'shopZtUuUv', 'passw56VetVTN', 'Garena', '6956520', 75, 7, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(17, 'Liên Minh', 1, 'shopPLS9Vb', 'passwH2xO3NQq', 'Garena', '9932881', 62, 174, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(18, 'Liên Minh', 1, 'shopufMyIJ', 'passwPtisAC5S', 'Garena', '2035397', 76, 351, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(19, 'Liên Minh', 1, 'shopXk8cKa', 'passwq1mQKXds', 'Garena', '19228997', 136, 33, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(20, 'Liên Minh', 1, 'shop5ff1hz', 'passwEVfkzTpm', 'Garena', '16393567', 59, 117, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(21, 'Liên Minh', 1, 'shopNOsJvo', 'passwIB6OqbQb', 'Garena', '2374630', 83, 217, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(22, 'Liên Minh', 1, 'shopHGUo6W', 'passwC9eSYupR', 'Garena', '13257206', 62, 332, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(23, 'Liên Minh', 1, 'shoplnmzZ8', 'passw8VfPX0pf', 'Garena', '679000', 25, 81, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(24, 'Liên Minh', 1, 'shopPGGefI', 'passwV1E7iCuw', 'Garena', '13651758', 44, 218, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(25, 'Liên Minh', 1, 'shopZTMzPM', 'passwHla3vyYH', 'Garena', '15299646', 24, 227, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(26, 'Liên Minh', 1, 'shopqJ4DNR', 'passwNsI7YY5C', 'Garena', '10339482', 49, 74, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(27, 'Liên Minh', 1, 'shop9H0lzl', 'passwtkzA2VMG', 'Garena', '5715939', 112, 192, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(28, 'Liên Minh', 1, 'shop2B6b3x', 'passwrrs4DSMG', 'Garena', '21311328', 138, 339, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(29, 'Liên Minh', 1, 'shopaKfrSz', 'passwy9LRXdlw', 'Garena', '1714500', 98, 494, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(30, 'Liên Minh', 1, 'shopVXaTEg', 'passw24468tHt', 'Garena', '459000', 75, 330, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(31, 'Liên Minh', 1, 'shop6y715R', 'passwM91asBPr', 'Garena', '5444225', 44, 111, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(32, 'Liên Minh', 1, 'shoph2yPGv', 'passwRpw8uxxQ', 'Garena', '6602032', 46, 230, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1004),
(33, 'Liên Minh', 1, 'shopAN4L91', 'passwk1GETIpa', 'Garena', '11572035', 18, 353, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(34, 'Liên Minh', 1, 'shopbw2uV7', 'passwdDNWbQ32', 'Garena', '338662', 13, 485, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(35, 'Liên Minh', 1, 'shop7eWofJ', 'passwyqX8pc0A', 'Garena', '1164796', 107, 151, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(36, 'Liên Minh', 1, 'shopKJ1U4I', 'passwaDHTdVp7', 'Garena', '4107914', 96, 111, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1004),
(37, 'Liên Minh', 1, 'shop6bhDCm', 'passwGe2kvgeQ', 'Garena', '123999', 118, 463, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(38, 'Liên Minh', 1, 'shopXwwTy3', 'passwJ6Xh3Ary', 'Garena', '343000', 125, 547, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(39, 'Liên Minh', 1, 'shopl8ufbi', 'passwtvHixQy8', 'Garena', '17078544', 122, 245, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(40, 'Liên Minh', 1, 'shoprhLwcw', 'passwGgO3HCBY', 'Garena', '11595422', 110, 430, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(41, 'Liên Minh', 1, 'shop2e7lV3', 'passw790pvk3e', 'Garena', '3372089', 135, 437, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(42, 'Liên Minh', 1, 'shopBBhs59', 'passwNgbJ41R5', 'Garena', '9685735', 101, 398, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(43, 'Liên Minh', 1, 'shopPW7PX4', 'passwgbIe22Td', 'Garena', '9119471', 120, 193, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(44, 'Liên Minh', 1, 'shopNes6EC', 'passw84B9VSPn', 'Garena', '3448538', 55, 122, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(45, 'Liên Minh', 1, 'shopN1sZvI', 'passwKSvFS8Bx', 'Garena', '17998484', 3, 37, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(46, 'Liên Minh', 1, 'shopPe5tId', 'passwzb1Y4VKz', 'Garena', '11874513', 75, 100, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(47, 'Liên Minh', 1, 'shopFDnNYm', 'passwdh52l1xi', 'Garena', '1315397', 52, 530, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1004),
(48, 'Liên Minh', 1, 'shopgqmkeg', 'passwCh3KPNut', 'Garena', '9332369', 114, 154, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(49, 'Liên Minh', 1, 'shopC3JDoD', 'passwv3CAC4ya', 'Garena', '11172263', 13, 119, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(50, 'Liên Minh', 1, 'shopeL3P4E', 'passwH5v6N6TP', 'Garena', '10098168', 21, 104, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(51, 'Liên Minh', 1, 'shoptg7fVi', 'passwWbznRuAw', 'Garena', '21025871', 56, 89, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(52, 'Liên Minh', 1, 'shoprWYSvN', 'passwFyDCcOo1', 'Garena', '6989005', 94, 84, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(53, 'Liên Minh', 1, 'shopynoraW', 'passwKmxEs8Iw', 'Garena', '450000', 38, 427, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(54, 'Liên Minh', 1, 'shopTO0aHw', 'passwiH0FK2RQ', 'Garena', '18001703', 92, 96, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1004),
(55, 'Liên Minh', 1, 'shopz9ItFP', 'passwOzNN02u8', 'Garena', '4347866', 1, 310, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(56, 'Liên Minh', 1, 'shopWeOLFC', 'passwFCWDCAvN', 'Garena', '469167', 143, 534, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(57, 'Liên Minh', 1, 'shop7UC83J', 'passwFQDR2ntk', 'Garena', '11434982', 144, 303, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(58, 'Liên Minh', 1, 'shopU8BinY', 'passwXPh3LwKP', 'Garena', '2943419', 99, 358, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(59, 'Liên Minh', 1, 'shop7l7zyK', 'passwC2AiU0zM', 'Garena', '19614744', 25, 421, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(60, 'Liên Minh', 1, 'shop7OGFgR', 'passwnuznwnhm', 'Garena', '14397078', 67, 2, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(61, 'Liên Minh', 1, 'shopIMiAa8', 'passwwDlwKBmo', 'Garena', '7884135', 12, 57, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(62, 'Liên Minh', 1, 'shopCpPmId', 'passwWZPy4uHy', 'Garena', '5856612', 19, 239, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(63, 'Liên Minh', 1, 'shop7hfy5A', 'passw9sfmkylZ', 'Garena', '3062161', 93, 265, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(64, 'Liên Minh', 1, 'shopIp8ZVp', 'passwSXooONsr', 'Garena', '7802094', 51, 391, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(65, 'Liên Minh', 1, 'shop3xBwRq', 'passwkHgCyQOa', 'Garena', '466128', 30, 232, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(66, 'Liên Minh', 1, 'shopwA7r9v', 'passwQyp6eDN4', 'Garena', '320000', 141, 137, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(67, 'Liên Minh', 1, 'shopwghNzU', 'passwBz3M40O5', 'Garena', '20216994', 35, 306, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1004),
(68, 'Liên Minh', 1, 'shopJ33TIU', 'passwlfSsVlP4', 'Garena', '11403187', 83, 312, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1004),
(69, 'Liên Minh', 1, 'shopDIvn8y', 'passw4MO3MlHh', 'Garena', '8887799', 69, 216, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(70, 'Liên Minh', 1, 'shop3DXH3L', 'passwx2U8AXzF', 'Garena', '18169377', 115, 239, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(71, 'Liên Minh', 1, 'shop7UyCEs', 'passwhrrz1mbx', 'Garena', '7119599', 49, 304, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(72, 'Liên Minh', 1, 'shopX8MoOv', 'passwaWv2yZJ6', 'Garena', '11285986', 28, 267, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(73, 'Liên Minh', 1, 'shopUw4PnW', 'passwdIsJOwH6', 'Garena', '5441113', 68, 141, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1004),
(74, 'Liên Minh', 1, 'shopoi4fkw', 'passwIs78pm1g', 'Garena', '21426481', 99, 106, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1004),
(75, 'Liên Minh', 1, 'shop13N6V6', 'passwdmxCtkOg', 'Garena', '1222586', 106, 457, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(76, 'Liên Minh', 1, 'shopzD0Ngc', 'passwvnc15LYu', 'Garena', '7751947', 89, 245, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(77, 'Liên Minh', 1, 'shopeccuQa', 'passwVMkZ5CsC', 'Garena', '17341767', 81, 485, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1004),
(78, 'Liên Minh', 1, 'shopNhpRWX', 'passwK2UZ2bXp', 'Garena', '20943535', 100, 476, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(79, 'Liên Minh', 1, 'shopwcU8m2', 'passw982vMQ2k', 'Garena', '110999', 111, 339, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(80, 'Liên Minh', 1, 'shopHiWiJD', 'passwgBoEIoSI', 'Garena', '3744674', 25, 245, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(81, 'Liên Minh', 1, 'shopTlzxPp', 'passw4UwlJq6L', 'Garena', '18294490', 81, 27, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1004),
(82, 'Liên Minh', 1, 'shopkVOgwk', 'passwqYR7BeNo', 'Garena', '2426438', 30, 446, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(83, 'Liên Minh', 1, 'shopEU5viN', 'passwUTnyQHTn', 'Garena', '18162764', 75, 395, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(84, 'Liên Minh', 1, 'shopSaeTaY', 'passwUD0d0Ly4', 'Garena', '6952157', 5, 498, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(85, 'Liên Minh', 1, 'shopSgEiQP', 'passwrdLk5DyI', 'Garena', '1102516', 74, 481, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(86, 'Liên Minh', 1, 'shopEOghx0', 'passwDRkmCxtw', 'Garena', '12877486', 87, 17, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(87, 'Liên Minh', 1, 'shopIUWwPp', 'passwaBhuGy9E', 'Garena', '21403613', 89, 538, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1004),
(88, 'Liên Minh', 1, 'shopn8GTWP', 'passwWP7uTg0Z', 'Garena', '9254477', 43, 505, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(89, 'Liên Minh', 1, 'shopu9XEK9', 'passwIC64wIB6', 'Garena', '2213702', 47, 469, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(90, 'Liên Minh', 1, 'shop8UpWf4', 'passwmodD1WtK', 'Garena', '12202806', 136, 226, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(91, 'Liên Minh', 1, 'shopS9krT3', 'passw5uxWHKbt', 'Garena', '10728676', 117, 405, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(92, 'Liên Minh', 1, 'shoph8qW29', 'passwIYGO5x1m', 'Garena', '11508128', 74, 363, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(93, 'Liên Minh', 1, 'shopL979Cp', 'passwxDRCZ1dt', 'Garena', '9513589', 96, 143, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(94, 'Liên Minh', 1, 'shoprqtz7H', 'passwfB9ha7Wa', 'Garena', '17010907', 59, 236, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(95, 'Liên Minh', 1, 'shopfbZAny', 'passwCIVZ5tGB', 'Garena', '13855853', 89, 276, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1004),
(96, 'Liên Minh', 1, 'shopuFXtb1', 'passw3JwkAoU6', 'Garena', '16123929', 89, 554, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(97, 'Liên Minh', 1, 'shopBPfksY', 'passwOK1WsZoR', 'Garena', '8818150', 121, 24, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(98, 'Liên Minh', 1, 'shopYwpYwT', 'passwGqzsTX5S', 'Garena', '11948535', 111, 552, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(99, 'Liên Minh', 1, 'shop3qhLwc', 'passwVEswvxqG', 'Garena', '6790976', 52, 540, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(100, 'Liên Minh', 1, 'shoptxKKgI', 'passw8BI3TFex', 'Garena', '5335101', 129, 75, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(101, 'Liên Minh', 1, 'shop64IyGM', 'passweupyEtEA', 'Garena', '7892511', 114, 457, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(102, 'Liên Minh', 1, 'shopNermrz', 'passwDPXb8VvA', 'Garena', '13033116', 112, 186, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1004),
(3456, 'Liên Minh', 1, 'shop557f1J', 'passwaM3oiuVp', 'Garena', '560686', 118, 76, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(3457, 'Liên Minh', 1, 'shopfree5K', 'passw4TSyPWhA', 'Garena', '3079209', 142, 104, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(3458, 'Liên Minh', 1, 'shopgRZa58', 'passwtwA4Jt85', 'Garena', '2268195', 99, 83, 'Bạch Kim', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(3459, 'Liên Minh', 1, 'shop47wC3u', 'passwcuvKiHri', 'Garena', '5200892', 111, 425, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(3460, 'Liên Minh', 1, 'shopavOCiL', 'passw5bzsWf7D', 'Garena', '13972897', 112, 147, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(3461, 'Liên Minh', 1, 'shopkbmtTv', 'passwLR3nDxxV', 'Garena', '13536152', 137, 173, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(3462, 'Liên Minh', 1, 'shopVFUgsA', 'passw3BGoFDar', 'Garena', '3333897', 63, 245, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1004),
(3463, 'Liên Minh', 1, 'shopAJXrbx', 'passwrfisbIhg', 'Garena', '98111', 121, 488, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(3464, 'Liên Minh', 1, 'shopn9RN2P', 'passwkzEZl8Fp', 'Garena', '13404574', 23, 455, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3465, 'Liên Minh', 1, 'shoptQTfMq', 'passwQ5keQpnY', 'Garena', '10504482', 149, 157, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3466, 'Liên Minh', 1, 'shop8KyKfg', 'passwDJKtrY7r', 'Garena', '2855642', 108, 89, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3467, 'Liên Minh', 1, 'shopaL4U6S', 'passwWOa2n8G5', 'Garena', '13501742', 122, 323, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(3468, 'Liên Minh', 1, 'shopEqw7oR', 'passwGQFplJ5l', 'Garena', '8736181', 95, 242, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(3469, 'Liên Minh', 1, 'shopUResGb', 'passwb83cZKHU', 'Garena', '111430', 125, 46, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(3470, 'Liên Minh', 1, 'shopUuJfik', 'passwL4FcwuVQ', 'Garena', '16396244', 51, 76, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3471, 'Liên Minh', 1, 'shopT5Wd1p', 'passwrV8pBQGw', 'Garena', '2240958', 66, 323, 'Vàng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3472, 'Liên Minh', 1, 'shopxIzeNp', 'passwOEAKVW1a', 'Garena', '1241797', 83, 416, 'Bạc', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3473, 'Liên Minh', 1, 'shopDGpUz5', 'passwrSU8GdOS', 'Garena', '450999', 119, 208, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(3474, 'Liên Minh', 1, 'shopqZc1yV', 'passwVza8feuH', 'Garena', '13706312', 71, 416, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(3475, 'Liên Minh', 1, 'shopcRS7Wc', 'passwlB1lfWul', 'Garena', '6520302', 60, 247, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(3476, 'Liên Minh', 1, 'shopND5fzw', 'passwMUWXfZRH', 'Garena', '222000', 69, 135, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb4.webp', 1003),
(3477, 'Liên Minh', 1, 'shopVkRJqL', 'passwoPq1VrhN', 'Garena', '140000', 19, 276, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1003),
(3478, 'Liên Minh', 1, 'shopMODpDD', 'passwSbu6ffuP', 'Garena', '6701262', 91, 513, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(3479, 'Liên Minh', 1, 'shopIQLIeM', 'passwVPTLpfHT', 'Garena', '925058', 67, 514, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb5.webp', 1003),
(3480, 'Liên Minh', 1, 'shopTW1DyG', 'passwRcxiq7Hp', 'Garena', '9064455', 68, 232, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003),
(3481, 'Liên Minh', 1, 'shopM9528L', 'passwUTkZu3Ko', 'Garena', '332000', 107, 429, 'Đồng', 'Trắng Thông Tin', '0', '/storage/images/default-thumb1.webp', 1003),
(3482, 'Liên Minh', 1, 'shop86Tw21', 'passwS7KsMUUp', 'Garena', '7035032', 52, 129, 'Kim Cương', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(3483, 'Liên Minh', 1, 'shopxGypr2', 'passwDbqv5LmC', 'Garena', '16052078', 103, 279, 'Sắt', 'Trắng Thông Tin', '0', '/storage/images/default-thumb6.webp', 1003),
(3484, 'Liên Minh', 1, 'shopSn1867', 'passw7hHECL0M', 'Garena', '1656031', 119, 449, 'Đại Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/default-thumb2.webp', 1003),
(3485, 'Liên Minh', 1, 'shopuPOqlm', 'passwA5dArPpb', 'Garena', '2738457', 75, 226, 'Cao Thủ', 'Trắng Thông Tin', '0', '/storage/images/0qBPw7AiOQ_1632531413.jpg', 1004),
(3486, 'Liên Minh', 1, 'shopJCIGCp', 'passw7ekqMUIQ', 'Garena', '230999', 22, 434, 'Chưa Rank', 'Trắng Thông Tin', '0', '/storage/images/default-thumb3.webp', 1003);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `status` text NOT NULL,
  `create_at` datetime NOT NULL,
  `update_at` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`order_id`, `user_id`, `product_id`, `status`, `create_at`, `update_at`) VALUES
(1, 3, 66, 'Paid', '2022-08-21 21:33:08', '2022-10-11 21:33:08'),
(2, 1, 79, 'Paid', '2022-09-18 21:33:08', '2022-10-11 21:33:08'),
(3, 1, 45, 'Paid', '2022-06-14 00:00:00', '2022-10-11 21:33:08'),
(4, 1, 65, 'Paid', '2022-10-14 00:00:00', '2022-10-11 21:33:08'),
(5, 2, 52, 'Paid', '2022-05-28 21:33:08', '2022-10-11 21:33:08'),
(6, 2, 30, 'Paid', '2022-05-09 21:33:08', '2022-10-11 21:33:08'),
(7, 3, 100, 'Paid', '2022-04-11 21:33:08', '2022-10-11 21:33:08'),
(8, 3, 4, 'Paid', '2022-03-11 21:33:08', '2022-10-11 21:33:08'),
(9, 1, 71, 'Paid', '2022-03-11 21:33:08', '2022-10-11 21:33:08'),
(10, 1, 72, 'Paid', '2022-09-11 21:33:08', '2022-10-11 21:33:08'),
(11, 3, 75, 'Paid', '2022-02-11 21:33:08', '2022-10-11 21:33:08'),
(15, 1, 3462, 'Paid', '2022-07-13 17:22:59', '2022-10-13 17:22:59'),
(16, 1, 87, 'Paid', '2022-08-13 17:24:39', '2022-10-13 17:24:39'),
(17, 1, 95, 'Paid', '2022-07-13 17:25:43', '2022-10-13 17:25:43'),
(18, 1, 3485, 'Paid', '2022-10-13 17:28:02', '2022-10-13 17:28:02'),
(19, 1, 36, 'Paid', '2022-10-13 17:28:43', '2022-10-13 17:28:43');

-- --------------------------------------------------------

--
-- Table structure for table `product_category`
--

CREATE TABLE `product_category` (
  `product_category_id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `category_name` text NOT NULL,
  `action` text DEFAULT NULL,
  `img_src` text NOT NULL,
  `total` int(11) NOT NULL DEFAULT 0,
  `note` int(11) DEFAULT NULL,
  `status` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `product_category`
--

INSERT INTO `product_category` (`product_category_id`, `category_id`, `category_name`, `action`, `img_src`, `total`, `note`, `status`) VALUES
(1, 1, 'Liên Minh', 'LienMinh', '/storage/images/lien-minh.gif', 132, 0, 1),
(2, 1, 'Thử Vận May Liên Minh', '#', '/storage/images/thu-van-may.gif', 442, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL,
  `role_name_vi` text NOT NULL DEFAULT '\'Thành Viên\'',
  `role_name_en` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`role_id`, `role_name_vi`, `role_name_en`) VALUES
(1, 'Thành Viên', 'Member'),
(100, 'Quản Trị Viên', 'Admin');

-- --------------------------------------------------------

--
-- Table structure for table `status`
--

CREATE TABLE `status` (
  `status_id` int(11) NOT NULL,
  `status_name_vi` text NOT NULL,
  `status_name_en` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `status`
--

INSERT INTO `status` (`status_id`, `status_name_vi`, `status_name_en`) VALUES
(1001, 'Bị vô hiệu hoá', 'Disable'),
(1002, 'Được kích hoạt', 'Enable'),
(1003, 'Chưa bán', 'Not sold'),
(1004, 'Đã bán', 'Sold'),
(1005, 'Đã xoá', 'Deleted'),
(1006, 'Tài khoản bị khoá', 'Ban'),
(1007, 'Tài khoản đang sử dụng', 'Active');

-- --------------------------------------------------------

--
-- Table structure for table `the_nap_data`
--

CREATE TABLE `the_nap_data` (
  `id` int(11) NOT NULL,
  `telecom_name` text NOT NULL,
  `Amount` decimal(11,0) NOT NULL,
  `pin` text NOT NULL,
  `serial` text NOT NULL,
  `is_use` tinyint(1) NOT NULL DEFAULT 0,
  `create_at` datetime NOT NULL,
  `update_at` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `the_nap_data`
--

INSERT INTO `the_nap_data` (`id`, `telecom_name`, `Amount`, `pin`, `serial`, `is_use`, `create_at`, `update_at`) VALUES
(92, 'VCOIN', '200000', '7178973631401', '1159695065735', 0, '2022-10-22 17:17:59', '2022-10-22 17:17:59'),
(93, 'VCOIN', '100000', '5739198889622', '6113079949294', 0, '2022-10-22 17:17:59', '2022-10-22 17:17:59'),
(94, 'VIETNAMMOBILE', '50000', '1272216520462', '2534056723284', 0, '2022-10-22 17:17:59', '2022-10-22 17:17:59'),
(95, 'VINAPHONE', '30000', '1016504701897', '2415201317684', 0, '2022-10-22 17:17:59', '2022-10-22 17:17:59'),
(96, 'ZING', '20000', '4947589313921', '1285370938686', 0, '2022-10-22 17:18:00', '2022-10-22 17:18:00'),
(97, 'VIETTEL', '500000', '9109873101140', '8484238505353', 1, '2022-10-22 17:18:00', '2022-10-22 17:18:00'),
(98, 'MOBIFONE', '50000', '4379465428019', '7546583424430', 0, '2022-10-22 17:18:00', '2022-10-22 17:18:00'),
(99, 'VIETTEL', '100000', '4185222447843', '6746002220201', 0, '2022-10-22 17:18:00', '2022-10-22 17:18:00'),
(100, 'VIETNAMMOBILE', '300000', '6906798637135', '3410400532665', 0, '2022-10-22 17:18:00', '2022-10-22 17:18:00'),
(101, 'ZING', '30000', '2573643452259', '1650906091414', 0, '2022-10-22 17:18:00', '2022-10-22 17:18:00');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `user_name` text NOT NULL,
  `password` text NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `phone` text DEFAULT NULL,
  `email` text NOT NULL,
  `money` decimal(10,0) NOT NULL DEFAULT 0,
  `role_id` int(11) NOT NULL DEFAULT 1,
  `status_id` int(11) NOT NULL,
  `img_src` text DEFAULT NULL,
  `cover_img_src` text NOT NULL,
  `note` text DEFAULT NULL,
  `create_at` datetime NOT NULL,
  `update_at` datetime NOT NULL,
  `last_login` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `user_name`, `password`, `first_name`, `last_name`, `phone`, `email`, `money`, `role_id`, `status_id`, `img_src`, `cover_img_src`, `note`, `create_at`, `update_at`, `last_login`) VALUES
(1, 'giangadmin', '0c40ca0420380a00b902308200d0cc05009a06f07508409b', 'giang', 'huy', '0981333332', 'giang@vtc.edu.vn', '58394163', 100, 1007, 'AdminAssets/img/userGiang.jpg', 'storage/images/unknown-cover.jpg', 'pw: 1', '2022-09-25 19:41:58', '2022-09-25 19:41:58', '2022-10-26 09:47:49'),
(2, 'giang', '0c40ca0420380a00b902308200d0cc05009a06f07508409b', 'firstName Change', '972022T192454', '0123456789', '28122002g@gmail.com', '36734', 1, 1007, 'storage/images/unknown-avatar.jpg', 'storage/images/unknown-cover.jpg', 'none', '2022-09-25 19:41:58', '2022-09-25 22:03:54', '2022-10-22 01:06:56'),
(3, 'tienngu1234', '09101000a03a0c30a20b20da08904106c0e10b00fd0c30a6', 'NickVn', '20220927T082541', '0999999999', 'tn@tn.tn', '523022', 1, 1007, 'storage/images/unknown-avatar.jpg', 'storage/images/unknown-cover.jpg', 'none', '2022-09-27 08:25:41', '2022-09-27 08:25:41', '2022-10-11 14:25:59'),
(4, 'dangxuantien12', '0870f60390090c00c805f0ef0c70120cb0530cd06308007b', 'NickVn', '20220927T084739', '0976315496', 'dangxuantien161202@gmail.com', '0', 1, 1006, 'storage/images/unknown-avatar.jpg', 'storage/images/unknown-cover.jpg', 'none', '2022-09-27 08:47:39', '2022-09-27 08:47:39', '2022-09-27 08:48:10'),
(5, 'admin8', '0c40ca0420380a00b902308200d0cc05009a06f07508409b', 'giang', 'huy', '0981333311', 'giaang@vtc.edu.vn', '58394163', 1, 1007, 'AdminAssets/img/userGiang.jpg', 'storage/images/unknown-cover.jpg', 'pw: 1', '2022-09-25 19:41:58', '2022-09-25 19:41:58', '2022-10-26 14:44:23');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`category_id`);

--
-- Indexes for table `charge_history`
--
ALTER TABLE `charge_history`
  ADD PRIMARY KEY (`phone_card_history_id`,`user_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `googlerecaptcha`
--
ALTER TABLE `googlerecaptcha`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`img_id`,`product_id`),
  ADD KEY `product_id` (`product_id`) USING BTREE;

--
-- Indexes for table `lienminh`
--
ALTER TABLE `lienminh`
  ADD PRIMARY KEY (`product_id`),
  ADD KEY `statuses_fk_status_id_lol` (`status_id`),
  ADD KEY `fk_02_product-category-id` (`product_category_id`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`,`user_id`,`product_id`),
  ADD KEY `oders_ibfk_1` (`user_id`),
  ADD KEY `orders_ibfk_2` (`product_id`);

--
-- Indexes for table `product_category`
--
ALTER TABLE `product_category`
  ADD PRIMARY KEY (`product_category_id`,`category_id`),
  ADD KEY `fk_04_categories_prod-category` (`category_id`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`role_id`);

--
-- Indexes for table `status`
--
ALTER TABLE `status`
  ADD PRIMARY KEY (`status_id`);

--
-- Indexes for table `the_nap_data`
--
ALTER TABLE `the_nap_data`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD KEY `statuses_fk_status_id` (`status_id`),
  ADD KEY `role_id` (`role_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `charge_history`
--
ALTER TABLE `charge_history`
  MODIFY `phone_card_history_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `googlerecaptcha`
--
ALTER TABLE `googlerecaptcha`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `images`
--
ALTER TABLE `images`
  MODIFY `img_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1477;

--
-- AUTO_INCREMENT for table `lienminh`
--
ALTER TABLE `lienminh`
  MODIFY `product_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45806;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `product_category`
--
ALTER TABLE `product_category`
  MODIFY `product_category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=101;

--
-- AUTO_INCREMENT for table `the_nap_data`
--
ALTER TABLE `the_nap_data`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `charge_history`
--
ALTER TABLE `charge_history`
  ADD CONSTRAINT `charge_history_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `images`
--
ALTER TABLE `images`
  ADD CONSTRAINT `images_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `lienminh` (`product_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `lienminh`
--
ALTER TABLE `lienminh`
  ADD CONSTRAINT `fk_02_product-category-id` FOREIGN KEY (`product_category_id`) REFERENCES `product_category` (`product_category_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `statuses_fk_status_id_lol` FOREIGN KEY (`status_id`) REFERENCES `status` (`status_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `lienminh` (`product_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `product_category`
--
ALTER TABLE `product_category`
  ADD CONSTRAINT `fk_04_categories_prod-category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_categories_prod-category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `statuses_fk_status_id` FOREIGN KEY (`status_id`) REFERENCES `status` (`status_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
