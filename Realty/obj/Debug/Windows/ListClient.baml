   M S B A M L   `   `   `  ����A  =Realty, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Realty.WindowsRealty.Windows  5   	       Realty.Windows.ListClient   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Realty.Windows     5   	     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	   $	��400��5
   	   $	��500��6    � Title$ 
ListClient��5   	   $	��800��6   $	��700��6)   .��5      �  � RowDefinitions 5   
   �� 5      $	��37* �6   �� 5      $
��188* �6   .+�5   
   �� ���#DCDFEE._�5      � $��Center=�5      $��Center=�5      .+�5      �� $��Поиск :��5      $��30(�5      $��Light"�5       �� TextAlignment$ Center=�5      �� 5      -   5         ��Name$ tbSearch��5      $��20(�5!      $��Light"�5"      $��10 0 0 0q�5#      $	��275��5$      $��42��5%      5(      5)      �� 5*   
   -   5         ��Name$ btnClost��5+      $��40��5,      $��40��6   $	��❌��5-      $��19(�5.      
������ $	��Top=�51      $��Right=�52      �� 53   
   -   5         ��Name$ lvClient��55      $��1��6    �� View 56      �� 57      .P�58      �� $��Код��6%   	 �� Width$
	 auto��62   
 �� DisplayMemberBinding
 6?   �� *
IDClient+�� 59      $��Фамилия��6%   $
	 auto��66   
 6C   �� *	Surname+�� 5:      $��Имя��6%   $
	 auto��62   
 6?   �� *	FirstName+�� 5;      $��Телефон��6%   $
	 auto��66   
 6C   �� *Phone+�� 5<      $!��Номер паспорта��6%   $
	 auto��6=   
 6J   �� *NumberPassport+�� 5=      $!��Серия паспорта��6%   $
	 auto��6=   
 6J   �� *SerialPassport+5?      5A      5B      5C      