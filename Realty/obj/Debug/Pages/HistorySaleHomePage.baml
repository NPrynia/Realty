   M S B A M L   `   `   `  ����A  =Realty, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Realty.PagesRealty.Pages  5      &     Realty.Pages.HistorySaleHomePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Realty.Pages     5        N� Title$  HistorySaleHomePage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $	��12* �6   �� 5      $	��60* �6   �� 5      $	��12* �6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   .+�5   
   �� $��2��5      ���#DCDFEE._�5      � $��Center=�5      $��Center=�5      .+�5      �� $��Поиск :��5      $��30(�5      $��Light"�5        �� TextAlignment$ Center=�5!      �� 5$      -   5         ��Name$ tbSearch��5%      $��20(�5&      $��Light"�5(      $��10 0 0 0q�5)      $	��275��5*      $��42��5+      �� 5.      $��Сортировать:��5/      $��30(�50      $��Light"�51      $��10 0 0 0q�52      �� 55      -   5         ��Name$ cbSort��56      $��20(�58      $��Light"�59      $��10 0 0 0q�5:      $	��275��5;      $��42��5<      5?      5@      �� 5A   
   -   5         ��Name$ 	lvHistory��5B      $��1��6    $��2��6-    �� View 5C      �� 5D      .P�5E      �� $��ID��6%    �� Width$
 auto��61   	 �� DisplayMemberBinding	 6>   �� *IDSaleHouse+�� 5F      $��ID дома��6%   $
 auto��66   	 6C   �� *	IDHouse+�� 5G      $��Адресс��6%   $
 auto��65   	 6B   �� *House.adressHouse+�� 5H      $��Адресс��6%   $
 auto��65   	 6B   �� *
House.Cost+�� 5I      $��ID Клиента��6%   $
 auto��69   	 6F   �� *
IDClient+�� 5J      $��Клиент��6%   $
 auto��65   	 6B   �� *
Client.FIO+�� 5K      $(��"Дата начала аренды��6%   $
 auto��6A   	 6N   �� *Date+
 �� StringFormat$
 {0:dd.MM.yyyy}���� 5L      $��Удалено��6%   $
 auto��66   	 6C   �� *
IsDelete+5N      5P      5Q      5R      