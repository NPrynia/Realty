   M S B A M L   `   `   `  ����A  =Realty, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:RealtyRealty  5   	       Realty.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Realty     5   	   D @ModernWpf, Version=0.9.4.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.4.0, Culture=neutral, PublicKeyToken=nullL HModernWpf.MahApps, Version=0.9.4.0, Culture=neutral, PublicKeyToken=null.ui!http://schemas.modernwpf.com/2019    6,     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	   $	��700��5
   	   $
��1200��6    � Title$ Главное окно��5   	   $	��800��6   $
��1390��6+   .��5      � .+�5   
   '  !ModernWpf.Controls.NavigationView  -   5          Name$ nv��5      ���#202136   PaneDisplayMode$
 Auto=�5         IsSettingsVisible .     	PaneTitle$ Недвижимость��5         OpenPaneLength$	 300Q�5         IsBackButtonVisible$ 	Collapsed=�5      	   	MenuItems	 5      +  %ModernWpf.Controls.NavigationViewItem  5      $��Покупка��6(   
   Icon-  'ModernWpf.Controls.IconElementConverter$

 shop 5   (    ^� ToolTip$- 'Покупка недвижимости��5   (      SelectsOnInvoked .     	MenuItems 5        5      $��Дома��60   $
 Street 6?   $ Покупка дома��6M   
 � Tag$ SamplePage3��6s     5       $��Квартиры��60   $

 home 6C   $% Покупка квартиры��6O   $ SamplePage4��6y   5"        5#      $��Аренда��6(   $
 Calendar 5$   (   $+ %Аренда недвижимости��5%   (    .   5(        5)      $��Дома��60   $
 Street 6?   $ Аренда дома��6M   $ SamplePage5��6r     5*      $��Квартиры��60   $

 home 6C   $# Аренда квартиры��6O   $ SamplePage6��6x   5,        5-      $)��#Действующие аренды��6(   $
 	GoToToday 5.   (   $) #Действующие аренды��5/   (    .  50   (     52      $/��)Добавить недвижимость��6(   $	
 Add 53   (   $/ )Добавить недвижимость��54   (    .  55   (     57      $��История��6(   $
 Library 58   (   $ История��59   (    .  5:   (      FooterMenuItems 5>        5?      $��
Выход��6(   $ 
Выход��5@   (   $ 
Выход��5A   (   $
 Clear 64   .��5D      � .+�5E        ModernWpf.Controls.Frame  -   5          Name$ contentFrame��5E   2   ���#DCDFEE�� 5F      -   5        $��
borderDrag��5H      $��Stretch=�5F      $��30��5G      $	��Top=�5I      
������ 5M      5N      5P      5Q      5R      