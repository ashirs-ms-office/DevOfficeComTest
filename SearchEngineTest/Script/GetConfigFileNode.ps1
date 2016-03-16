Param(
    [string]$nodeName
    )
 #   write-host $nodeName
#----------------------------------------------------------------------------
# Get the value of the node
#----------------------------------------------------------------------------       
[xml]$configContent = New-Object XML
$configContent.Load(".\AutoTestConfig.xml")
$propertyNodes = $configContent.GetElementsByTagName("Property")
foreach($node in $propertyNodes)
{
    if($node.GetAttribute("name") -eq $nodeName)
    {
        $value=$node.GetAttribute("value")
       #write-host $value
        return $value
    }
}