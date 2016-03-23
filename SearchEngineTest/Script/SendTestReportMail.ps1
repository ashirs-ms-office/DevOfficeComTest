param(
[String] $TestOutputFile,
[String] $UserandMachine,
[String] $Date,
[String] $Time)

function SubStringCount
{
    param(
    [string]$str,
    [string]$subStr
    )

    $count=0;    
    if ($str.Contains($subStr))
    {
        $count++;
        $strReplaced = $str.Replace($subStr, "");   
        return ($str.Length-$strReplaced.Length)/ $subStr.Length;            
    }
    return 0;
}

function ExtractKeyWordsAndRanking
{
    param(
    [string]$traceInfo,
    [int]$count
    )
    $searchedArr=@();
    $replacedString=$traceInfo
    
    for($i=0;$i -lt $count;$i++)
    {
        $keyWordsIndex=$replacedString.LastIndexOf("Keywords:")+9;
        $rankingIndex=$replacedString.LastIndexOf("Ranking:")+8;
        
        $searchedArr+=$replacedString.Substring($keyWordsIndex,$replacedString.LastIndexOf("Ranking:")-$keyWordsIndex)+"<->"+$replacedString.Substring($rankingIndex);
        $replacedString=$replacedString.Substring(0,$replacedString.LastIndexOf("Keywords:"));
    }
    for($j=0;$j -lt $searchedArr.Length/2;$j++)
    {
    $temp=$searchedArr[$j];
    $searchedArr[$j]=$searchedArr[$searchedArr.Length-$j-1];
    $searchedArr[$searchedArr.Length-$j-1]=$temp;
    }
    return $searchedArr;
}

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
$traceFileName= $UserandMachine+" "+$Date+" "+$Time
[xml]$traceContent = New-Object XML
$traceContent.Load($traceFileName)
$caseResultNodes = $traceContent.GetElementsByTagName("UnitTestResult")

$contents = Get-Content -Path $TestOutputFile
$mail.Body='<table cellspacing="1" cellpadding="3" border="3" style="font-size: 12pt;line-height: 15px;border-left:3px;">';
$mail.Body+='<tr style="background-color:#8B008B;color:#FFFFFF"><th>Test Case Name</th><th>Search Key Words</th><th>Ranking</th><th>Search Engine</th><th>Result</th></tr>';
$mail.Body+='<tr>';
for($i=0;$i -lt $contents.Length; $i++)
{
    if($contents[$i])
    {
        if($contents[$i].StartsWith("Fail"))
        {
            $mail.Priority = "High"
        }
        if($contents[$i].StartsWith("Failed") -or $contents[$i].StartsWith("Passed") -or $contents[$i].StartsWith("Inconclusive"))
        {
            $result=$contents[$i].Split(" ")[0]
            $searchCount=0;
            $searchedArr=@();
            #Add Case name
            $caseName=$contents[$i].SubString($result.Length).Trim()
            foreach($caseResultNode in $caseResultNodes)
            {
                if($caseResultNode.GetAttribute("testName") -eq $caseName)
                {
                    $traceInfo=$caseResultNode.GetElementsByTagName("StdOut")[0].InnerText;
                    $searchCount=SubStringCount $traceInfo "Keywords"
                    $searchedArr+=ExtractKeyWordsAndRanking $traceInfo $searchCount
                }
            }
            
            $mail.Body+="<td rowspan=`"$searchCount`">";
            $mail.Body+=$caseName;
            $mail.Body+='</td>';
                        
            #Add Key Words
            $mail.Body+='<td>';
            $mail.Body+=$searchedArr[0].Replace("<->",'</td><td>');
            $mail.Body+='</td>';         
            
            #Add Search Engine
            $mail.Body+="<td rowspan=`"$searchCount`">";
            $searchEngine=.\GetConfigFileNode.ps1 "SearchEngine" $true
            $mail.Body+=$searchEngine;
            $mail.Body+='</td>';
            #Add Result
            if($result -eq "Passed")
            {
                $mail.Body+="<td rowspan=`"$searchCount`"><p style=`"color:#008000;`">";
            }
            elseif($result -eq "Failed")
            {
                $mail.Body+="<td rowspan=`"$searchCount`"><p style=`"color:#FF0000;`">";
            }
            else
            {
                $mail.Body+="<td rowspan=`"$searchCount`"><p style=`"color:#FFFF00;`">";
            }
            $mail.Body+=$result;
            $mail.Body+='</p></td>';
            $mail.Body+='</tr>';
            
            for($k=1;$k -lt $searchCount;$k++)
            {
                $mail.Body+='<tr><td>'+$searchedArr[$k].Replace("<->",'</td><td>')+'</td></tr>';
            }
        }
    }
}
$mail.Body+='</table>';

$mail.Body+='<div style="font-size: 12pt;"> <br/>For details please see the attachment.</div>';

#Add the trace file as attachment
$attachment = new-Object System.Net.Mail.Attachment($traceFileName)
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