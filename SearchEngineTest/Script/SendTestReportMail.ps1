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
$mail.IsBodyHtml=$true
$mail.Subject = "[SeachEngine]Automation Test Report"+"_"+$Date+"_"+$Time.Split(".")[0]

#Construct the mail table
$contents = Get-Content -Path $TestOutputFile
$mail.Body='<table cellspacing="1" cellpadding="3" border="3" style="font-size: 12pt;line-height: 15px;border-left:3px;">';
$mail.Body+='<tr style="background-color:#8B008B;color:#FFFFFF"><th>Test Case Name</th><th>Search Key Words</th><th>Search Engine</th><th>Ranking</th><th>Result</th></tr>';
for($i=0;$i -lt $contents.Length; $i++)
{
    if($contents[$i])
    {
        if($contents[$i].StartsWith("Fail"))
        {
            $mail.Priority = "High"
        }
        if($contents[$i].StartsWith("Fail") -or $contents[$i].StartsWith("Passed") -or $contents[$i].StartsWith("Inconclusive"))
        {
            $result=$contents[$i].Split(" ")[0]
            
            $mail.Body+='<tr><td>';
            #Add Case name
            $caseName=$contents[$i].SubString($result.Length).Trim()
            $mail.Body+=$caseName;
            $mail.Body+='</td><td>';
            #Add Key Words
            $mail.Body+='###';
            $mail.Body+='</td><td>';
            #Add Search Engine
            $searchEngine=.\GetConfigFileNode.ps1 "SearchEngine" $true
            $mail.Body+=$searchEngine;
            $mail.Body+='</td><td>';
            #Add Ranking
            $mail.Body+='###';
            $mail.Body+='</td>';
            #Add Result
            if($result -eq "Passed")
            {
                $mail.Body+='<td><p style="color:#008000;">';
            }
            elseif($result -eq "Fail")
            {
                $mail.Body+='<td><p style="color:#FF0000;">';
            }
            else
            {
                $mail.Body+='<td><p style="color:#FFFF00;">';
            }
            $mail.Body+=$result;
            $mail.Body+='</p></td></tr>';
        }
    }
}
$mail.Body+='</table>';

$mail.Body+='<div style="font-size: 12pt;"> <br/>For details please see the attachment.</div>';

#Add the trace file as attachment
$filename= $UserandMachine+" "+$Date+" "+$Time
$attachment = new-Object System.Net.Mail.Attachment($filename)
$mail.Attachments.Add($attachment)

#Send the message
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