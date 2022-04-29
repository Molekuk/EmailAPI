# EmailAPI - сервис по отправке email сообщений
<h3>/api/mails (GET) - возвращает все отправленные сообщения</h3>
<h3>/api/mails (POST) - принимает запрос в формате Json и отправляет сообщение</h3>
<h4>Модель запроса:<br>
{<br>
  "subject": "string",<br>
  "body": "string",<br>
  "recipients": ["string"]<br>
}<br>
</h4>
<h4>API протестировано при помощи Postman</h4>
