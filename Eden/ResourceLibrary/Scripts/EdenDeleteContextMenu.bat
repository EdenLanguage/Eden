@echo off
reg delete "HKEY_CLASSES_ROOT\.eden" /f
reg delete "HKEY_CLASSES_ROOT\Directory\Background\Shell\Create Eden File" /f
reg delete "HKEY_CLASSES_ROOT\Directory\Shell\Create Eden File" /f