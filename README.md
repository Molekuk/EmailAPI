# EmailAPI - сервис по отправке email сообщений
<h4>/api/mails (GET) - возвращает все отправленные сообщения</h4>
<h4>/api/mails (POST) - принимает запрос в формате Json и отправляет сообщение</h4>
<h5>Модель запроса:<br>
{<br>
  "subject": "string",<br>
  "body": "string",<br>
  "recipients": ["string"]<br>
}<br>
</h5>
<h4>API протестировано при помощи Postman</h4>
