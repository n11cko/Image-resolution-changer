   M S B A M L   `   `   `  ����N  Jimage_converter_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:image_converter_wpfimage_converter_wpf  5   	   $    image_converter_wpf.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   P LHaley.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=9b5f4902d3fea17a+hw"http://schemas.hpod9.com/haley/wpf  5   	   Q MHaley.MVVM, Version=4.0.2.0, Culture=neutral, PublicKeyToken=9b5f4902d3fea17a,hm#http://schemas.hpod9.com/haley/mvvm  5   	   -local!clr-namespace:image_converter_wpf  5   	   -   5          �Name$  Image_Resizer��5   	    � Title$ Image Resolution Resizer��5
   	   $	��380��6*   $	��350��67    � 
ResizeMode$ NoResize=�6C    � Icon$d ^/web-icon-line-on-white-background-image-for-web-presentation-logo-icon-symbol-free-vector.jpg��6Y   .��5      � 
����ح� � ColumnDefinitions 5   
   �� 5      $	��47* �6   �� 5      $
��303* �6    � RowDefinitions 5   
   �� 5      $	��37* �6   �� 5      $	��81* �6   �� 5      $	��29* �6   .+�5   
   � $��2��6   $��	0,0,0,200q�6#   $��2��66    5      �� 5      $��1* �6#   60   �� 5      $��1* �6#   60   .+�5      ��  �� Orientation$ Vertical=�6   $
��Left=�60   .+�5!      �� $��18(�6   $
��Left=�6'   $	��Top=�6B   $��	28,13,0,0q�6Z   $��34��6n   $	��105��6z   .��6�   Source path5#      �� 5%      -   5        $��btnSpath��5&      $��Choose path��5%      $��70��5&   7   $��30��6B   $
��Left=�5'      $��Bottom=�6,   $��	46,0,0,10q�5(      5*      5+      �� 5.      $��1��6   $ Vertical=�6)   $��Right=�6A   .+�5/      �� $��18(�6   $��Right=�6'   $	��Top=�6C   $��	0,13,10,0q�6[   $��34��6n   $	��142��6z   .��6�   Destination path51      �� 53      -   5        $��btnDpath��54      $��Choose path��53      $��70��54   <   $��30��6G   $��Right=�55      $��Bottom=�6-   $��	0,0,46,10q�56      58      59      5:      �� 5E   
   -   5        $��
sourcePath��5E   (   $y�Hidden=�6   $��258,0,12,64q�6:    J� RenderTransformOrigin$ 0.543,0.567&�6O   $��1��6s   source6�   �� 5F   
   -   5        $��destinationPath��5F   (   $y�Hidden=�6   $��
10,10,9,59q�6?   dest6Y   �� 5H   
   $��1��6   $
��Left=�6   $��18(�68   $��
81,5,0,112q�6F   $��94��6Z   $��1��6e   .��6u   
Resolution5J      �� 5K   
   -   5        $��isPng��5L      $��1��6    $!��Keep transparency for *.png��5M      $��12(�5N      $��	36,70,0,0q�6!   $
��Left=�5O      $	��Top=�6.   $	��184��6F   $��1��6R   5P      �� 5Q   
   -   5        $��selectedResolution��5R      $
��Left=�5Q      $	��Top=�6.   $��1��5R   -   $��30��6:   $	��190��6F   $��	33,35,0,0q�5S      $��1��6&   .@�5T      ��  �� 
IsSelected$
 True��6   .��6-   50x5064   �� 5U      .��6   	100x1006$   �� 5V      .��6   	200x2006$   �� 5W      .��6   	250x2506$   �� 5X      .��6   	300x3006$   �� 5Y      .��6   	400x4006$   �� 5Z      .��6   	400x4006$   �� 5[      .��6   	450x4506$   �� 5\      .��6   	500x5006$   �� 5]      .��6   	550x5506$   �� 5^      .��6   	600x6006$   5_      �� 5b   
   -   5        $��	clrPicker��5c      $��Choose color��5b      $	��176��5c   :   $��40��6F   $
��Left=�5d      $	��Top=�6-   $��	40,94,0,0q�5e      $��1��6$   $��1��61   � 5g   
   -	   5        $��progressBar��5i      $��30��5g      $	��174��6"   $	��Top=�5h      $
��Left=�6.   	 � Minimum$	 0Q�5i   )   $��1��5j      $��
40,160,0,0q�6#   $��1��67   �� 5l   
   -
   5        $��btnStart��5m      $��Start��5l      $	��120��5m   8   $��42��6D   $
��Left=�5n      $��Center=�6-   $��2��6H   $��1��6U   $��68,0,0,0q�6e   5o      5q      