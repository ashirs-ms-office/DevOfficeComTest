param(
[String] $TestOutputFile,
[String] $UserandMachine,
[String] $Date,
[String] $Time)

Get-Location
#mail server configuration 
$smtpServer = .\GetConfigFileNode.ps1 "SMTPServerHost" 
$smtpUser = .\GetConfigFileNode.ps1 "SMTPUser"
$smtpPassword = .\GetConfigFileNode.ps1 "SMTPPassword"
$domain=.\GetConfigFileNode.ps1 "Domain"
$MailAddress =.\GetConfigFileNode.ps1 "FromAddress"
$toAddresses =(.\GetConfigFileNode.ps1 "ToAddresses").Split(";")
#create the mail message
$mail = New-Object System.Net.Mail.MailMessage

#set the addresses
$mail.From = New-Object System.Net.Mail.MailAddress($MailAddress)
foreach($toAddress in $toAddresses)
{
    $mail.To.Add($toAddress)
}
$mail.IsBodyHtml=$false
$mail.Subject = "[SeachEngine]Automation Test Report"+"_"+$Date+"_"+$Time.Split(".")[0]
$mail.Priority = "High"

#Adjust body format
$contents = Get-Content -Path $TestOutputFile
$bodyString="";
for($i=0;$i -lt $contents.Length; $i++)
{
    if($contents[$i])
    {
        $bodyString+=$contents[$i]+"`r";
    }
}

$mail.Body=$bodyString

$filename= $UserandMachine+" "+$Date+" "+$Time
$attachment = new-Object System.Net.Mail.Attachment($filename)
$mail.Attachments.Add($attachment)

#send the message
$smtpClient = New-Object System.Net.Mail.SmtpClient -argumentList $smtpServer
$smtpClient.Credentials = New-Object System.Net.NetworkCredential -argumentList $smtpUser,$smtpPassword,$domain
$smtpClient.EnableSsl = $true; 
$smtpClient.Timeout=60000;
[System.Net.ServicePointManager]::ServerCertificateValidationCallback = { return $true }
try { 
    $smtpClient.Send($mail) 
    write-host 'Test report mail is sent Successfully!' 
   } 

   catch { 
   write-host $_ 
   }