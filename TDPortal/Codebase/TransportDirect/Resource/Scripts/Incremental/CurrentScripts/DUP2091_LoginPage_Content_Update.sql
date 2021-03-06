-- ***********************************************
-- NAME 		: DUP2091_LoginPage_Content_Update.sql
-- DESCRIPTION 	: Login Register page content update
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Jan 2014
-- ************************************************

USE [Content]
GO
------------------------------------------------

EXEC AddContent 1, 'loginregister', 'en', 'TDPageInformationHtmlPlaceHolderDefinition', '/Channels/TransportDirect/LoginRegister',
'<div class="PageSoftContentContainer">
  <div class="PageSoftContent">
    <p>Registering with Transport Direct is optional. You can still
    access most of our features without registering but once
    registered you can:</p>
    <br />
    <ul>
      <li>Save your favourite journeys so you can access them again
      in the future without having to re-enter any
      information.</li>
      <li>Save travel preferences when entering information for a
      journey so you don''t have to enter them again - but you still
      have the option to change them if you wish.</li>
      <li>Email travel options to other people.</li>
	  <li>Extend the length of your session, so if you need more time to carry out certain tasks you can have it.</li>
    </ul>
    <br />
    <p>It is easy to register, simply enter your email address in
    the box above, then choose a password between 4 and 12
    characters, retype your password, click register and you''re
    done.</p>
    <br />
    <p>If at any time you wish to change your registered email
    address simply log on using your current address and password
    then click the ''Update email'' button and enter your new
    address.</p>
    <br />
  </div>
</div>
'

EXEC AddContent 1, 'loginregister', 'cy', 'TDPageInformationHtmlPlaceHolderDefinition', '/Channels/TransportDirect/LoginRegister',
'<div class="PageSoftContentContainer">
  <div class="PageSoftContent">
    <p>Registering with Transport Direct is optional. You can still
    access most of our features without registering but once
    registered you can:</p>
    <br />
    <ul>
      <li>Save your favourite journeys so you can access them again
      in the future without having to re-enter any
      information.</li>
      <li>Save travel preferences when entering information for a
      journey so you don''t have to enter them again - but you still
      have the option to change them if you wish.</li>
      <li>Email travel options to other people.</li>
	  <li>Extend the length of your session, so if you need more time to carry out certain tasks you can have it.</li>
    </ul>
    <br />
    <p>It is easy to register, simply enter your email address in
    the box above, then choose a password between 4 and 12
    characters, retype your password, click register and you''re
    done.</p>
    <br />
    <p>If at any time you wish to change your registered email
    address simply log on using your current address and password
    then click the ''Update email'' button and enter your new
    address.</p>
    <br />
  </div>
</div>
'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2091, 'Login Register page content update'

GO