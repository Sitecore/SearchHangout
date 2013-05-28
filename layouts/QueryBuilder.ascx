<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QueryBuilder.ascx.cs" Inherits="seven.layouts.QueryBuilder" %>
<%@ Register TagPrefix="sc" Namespace="Sitecore.Web.UI.WebControls" Assembly="Sitecore.Kernel" %>
<style>
    div
    {
        padding: 0px 0px 0 70px;
        position: relative;
        *z-index: -1;
        margin-left: -56px;
        margin-right: 220px;
    }

        div table a:link
        {
            color: #666;
            font-weight: bold;
            text-decoration: none;
        }

        div table a:visited
        {
            color: #999999;
            font-weight: bold;
            text-decoration: none;
        }

        div table a:active,
        div table a:hover
        {
            color: #bd5a35;
            text-decoration: underline;
        }

        div table
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #666;
            font-size: 12px;
            text-shadow: 1px 1px 0px #fff;
            background: #eaebec;
            border: #ccc 1px solid;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            margin: 27px auto;
            -moz-box-shadow: 0 1px 2px #d1d1d1;
            -webkit-box-shadow: 0 1px 2px #d1d1d1;
            box-shadow: 0 1px 2px #d1d1d1;
        }

            div table th
            {
                padding: 7px 25px 7px 25px;
                border-top: 1px solid #fafafa;
                border-bottom: 1px solid #e0e0e0;
                background: #ededed;
                background: -webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb));
                background: -moz-linear-gradient(top, #ededed, #ebebeb);
            }

                div table th:first-child
                {
                    text-align: left;
                    padding-left: 20px;
                }

            div table tr:first-child th:first-child
            {
                -moz-border-radius-topleft: 3px;
                -webkit-border-top-left-radius: 3px;
                border-top-left-radius: 3px;
            }

            div table tr:first-child th:last-child
            {
                -moz-border-radius-topright: 3px;
                -webkit-border-top-right-radius: 3px;
                border-top-right-radius: 3px;
            }

            div table tr
            {
                text-align: left;
                padding-left: 20px;
                cursor: pointer;
                /*PIE pie_hover not works here*/
                behavior: expression(runtimeStyle.behavior = 'none', onmouseover = function() {this.className += ' hover'}, onmouseout = function() {this.className = this.className.replace(/ hover/g, '')});
            }

                div table tr td:first-child
                {
                    text-align: left;
                    padding-left: 20px;
                    border-left: 0;
                }

                div table tr td
                {
                    padding: 7px;
                    border-top: 1px solid #ffffff;
                    border-bottom: 1px solid #e0e0e0;
                    border-left: 1px solid #e0e0e0;
                    background: #fafafa;
                }

                div table tr.even td
                {
                    background: #f6f6f6;
                    background: -webkit-gradient(linear, left top, left bottom, from(#f8f8f8), to(#f6f6f6));
                    background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
                }

                div table tr:last-child td
                {
                    border-bottom: 0;
                }

                    div table tr:last-child td:first-child
                    {
                        -moz-border-radius-bottomleft: 3px;
                        -webkit-border-bottom-left-radius: 3px;
                        border-bottom-left-radius: 3px;
                    }

                    div table tr:last-child td:last-child
                    {
                        -moz-border-radius-bottomright: 3px;
                        -webkit-border-bottom-right-radius: 3px;
                        border-bottom-right-radius: 3px;
                    }

                div table tr:hover td, #results table tr.hover td
                {
                    background: -webkit-gradient(linear, left top, left bottom, from(#f2f2f2), to(#f0f0f0));
                    background: -moz-linear-gradient(top, #f2f2f2, #f0f0f0);
                    background: gradient(linear, top, bottom, #F2F2F2, #f0f0f0);
                    behavior: url('/sitecore/shell/Applications/Buckets/Scripts/PIE.htc');
                }
</style>
<div style="height: 1000px">
    <h1>Query Comparer</h1>
    <div id="results">
        <table>
            <tbody>
                <tr>
                    <td>Test Name</td>
                    <td>LINQ</td>
                    <td>Query</td>
                    <td>Item</td>
                    <td>SearchManager</td>
                    <td>Fast Query</td>

                </tr>
                <tr>
                    <td>Where TemplateName Equals</td>
                    <td>
                        <asp:Literal ID="LinqTest1" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest1" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest1" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest1" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest1" runat="server"></asp:Literal></td>





                </tr>
                <tr>
                    <td>Where TemplateName Contains</td>
                    <td>
                        <asp:Literal ID="LinqTest2" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest2" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest2" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest2" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest2" runat="server"></asp:Literal></td>

                </tr>
                <tr>
                    <td>Get Children</td>
                    <td>
                        <asp:Literal ID="LinqTest3" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest3" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest3" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest3" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Get Descendants</td>
                    <td>
                        <asp:Literal ID="LinqTest4" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest4" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest4" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest4" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest4" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Get Ancestors</td>
                    <td>
                        <asp:Literal ID="LinqTest5" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest5" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest5" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest5" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest5" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Where Created Date Between</td>
                    <td>
                        <asp:Literal ID="LinqTest6" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="QueryTest6" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="ItemTest6" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="SearchTest6" runat="server"></asp:Literal></td>
                    <td>
                        <asp:Literal ID="FastQueryTest6" runat="server"></asp:Literal></td>
                </tr>
            </tbody>

        </table>


    </div>

    <div style="float: left">


        <table>
            <tbody>
                <tr>
                    <td>Field</td>
                    <td>Value</td>

                </tr>
                <tr>
                    <td>Base Sitecore Item Count</td>
                    <td>
                        10531</td>
                </tr>
                <tr>
                    <td>Item Count</td>
                    <td>
                        <asp:Literal ID="GlobalCount" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Start Location</td>
                    <td>/sitecore</td>
                </tr>
                 <tr>
                    <td>Repeat Run Count</td>
                    <td>3</td>
                </tr>
            </tbody>

        </table>
    </div>

</div>
