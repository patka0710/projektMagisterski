   M S B A M L   `   `   `  ���� Z  VProjektMagisterskiPatrycjaTkocz, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:ProjektMagisterskiPatrycjaTkoczProjektMagisterskiPatrycjaTkocz  0    *ProjektMagisterskiPatrycjaTkocz.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ?local-clr-namespace:ProjektMagisterskiPatrycjaTkocz       � Title$  
MainWindow��$	��370��$	��535��.��� $��
-1,0,-7,-6q�.+��� -    ��Name$ textBox��$
��Left=�$��23��$��	25,20,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��{�.  �� -    ��Name$	 run��$��Wykonaj��$
��Left=�$��430,290,0,0q�$	��Top=�$��75���� -    ��Name$ groupBox��$��Ustawienia ogólne��$
��Left=�$��
320,10,0,0q�$	��Top=�$��85��$	��185��.��� $
��Left=�$��60��$��5,5,0,-2q�$	��Top=�$	��165�� � ColumnDefinitions �� $	��34* ��� $
��131* �.+��� -    ��Name$ comboBox��$
��Left=�$��45,9,0,0q�$	��Top=�$��85�� �� SelectedValuePath$ nameFile��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��1������ *Data+���� .R��� ���� *
nameFile+�� -   	 ��Name$	 label1��$��Zbiór danych:��$
��Left=�$��-1,4,0,0q�$	��Top=�$��2���� -   $	 label��$��Ilość klastrów��$
��Left=�$��	-1,29,0,0q�$	��Top=�$��90��$��2���� -   $ numberOfCluster��$
��Left=�$��23��$��
100,34,0,0q�$
 Wrap=�$��3��$	��Top=�$��25��
 J� RenderTransformOrigin$
 0.5,0.5&� �� TextAlignment$ Right=�$��1���� -   $ 	groupBox1��$��Ustawienia K-średnich��$
��Left=�$��320,105,0,0q�$	��Top=�$��55��$	��185��.��� $
��Left=�$��35��$��5,0,0,-2q�$	��Top=�$	��165��.+��� -	   $	 label2��$��Ilość iteracji��$
��Left=�$��4,3,0,0q�$	��Top=��� -
   $ numberOfIteration��$
��Left=�$��23��$��	134,6,0,0q�$
 Wrap=�$��3��$	��Top=�$��25��$
 	0.6,1.957&�$ Right=��� -   $ 	groupBox2��$��Ustawienia PSO��$
��Left=�$��320,170,0,0q�$	��Top=�$	��110��$	��185��.��� $
��Left=�$��90��$��0,0,0,-2q�$	��Top=�$	��170��.+��� -   $	 label3��$��Ilość iteracji��$
��Left=�$��4,4,0,0q�$	��Top=��� -   $ numberOfIterationPSO��$
��Left=�$��23��$��	139,4,0,0q�$
 Wrap=�$	��100��$	��Top=�$��30��$
 	1.5,0.435&��� -   $	 label4��$��Ilość cząsteczek��$
��Left=�$��4,24,0,0q�$	��Top=�$
 -0.263,-1.154&��� -   $ numberOfParticles��$
��Left=�$��23��$��
139,29,0,0q�$
 Wrap=�$��10��$	��Top=�$��30���� -    ��Name$ listBoxResults��$
��Left=�$	��240��$��	25,65,0,0q�$	��Top=�$	��265��