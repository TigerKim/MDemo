<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PartyInvites.Models.ChangePasswordModel>" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
    암호 변경
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>암호 변경</h2>
    <p>
        암호를 변경하려면 아래 폼을 사용하십시오. 
    </p>
    <p>
        새 암호는 <%: ViewData["PasswordLength"] %>자 이상이어야 합니다.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "암호를 변경하지 못했습니다. 오류를 수정한 후 다시 시도하십시오.") %>
        <div>
            <fieldset>
                <legend>계정 정보</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.OldPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.OldPassword) %>
                    <%: Html.ValidationMessageFor(m => m.OldPassword) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.NewPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.NewPassword) %>
                    <%: Html.ValidationMessageFor(m => m.NewPassword) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                
                <p>
                    <input type="submit" value="암호 변경" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
