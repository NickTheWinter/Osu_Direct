# Osu Direct

Проект ***Osu Direct*** предназначен для получения функциональной возможности osu!direct из игры, так же, как это реализовано в **osu!lazer**.

## Установка

```
Переходим в релизные версии
```
![image](https://user-images.githubusercontent.com/80089119/220378160-2c34885a-4d51-4cde-a45b-84499d192843.png)

***
```
Качаем и разархивируем архив
```
![image](https://user-images.githubusercontent.com/80089119/220378585-ec4f12f2-abab-45b6-914c-67e3dbf962db.png)

***
```
Запускаем .exe файл

```
![image](https://user-images.githubusercontent.com/80089119/220382600-31359697-305c-41f7-8e7c-51025cd2eb27.png)

## Начало работы

После запуска программы появится небольшое окно, которое можно свернуть и оно будет помещено в трей.

Для того, чтобы увидеть результат этой экзекуции запустите osu! и нажмите сочетаний клавиш **CTRL** + **D**.

## Итог
![image](https://user-images.githubusercontent.com/80089119/220385217-43c6f62f-48ae-42cf-a76c-c91aa486e9d8.png)

### Дополнительная настройка

Если необходимо поменять сочетания клавиш, необходимо редактировать код самой программы. Необходимо скопировать название переменной, содержащую код необходимой ваш клавиши из списка ниже:

```C#
private const uint MOD_NONE = 0x0000; //(none)
private const uint MOD_ALT = 0x0001; //ALT
private const uint MOD_CONTROL = 0x0002; //CTRL
private const uint MOD_SHIFT = 0x0004; //SHIFT
private const uint MOD_WIN = 0x0008; //WINDOWS
private const uint VK_D = 0x0044; //CAPS LOCK:
```

И вставить его заместо кода ниже:
```C#
RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_D);
```

### Требования

* Windows x64
  * .Net Core 5.0
  * WebView2
