;#DropDB ExportSampleEasyTest

#Application ExportSampleWin
#Application ExportSampleWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef ExportSampleWin

*Action About...
 
*Action OK

#EndIf

#IfDef ExportSampleWeb
*Action Navigation(My Details)
#EndIf
