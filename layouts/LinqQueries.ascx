<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LinqQueries.ascx.cs" Inherits="seven.layouts.LinqQueries" %>
<div>
    <h1>LINQ Queries</h1>
    <p>
        <h2>Example 1 - Equals</h2>
        <code>
            <asp:Literal ID="Literal1" runat="server" Text="var query = context.GetQueryable<Article>().Where(item => item.Name == 'News Article');"></asp:Literal>
        </code>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </p>
    <p>
        <h2>Example 2 - Contains</h2>
        <code>
            <asp:Literal ID="Literal2" runat="server" Text="var query = context.GetQueryable<Article>().Where(item => item.Title.Contains('News Article'));"></asp:Literal>
        </code>
        <asp:GridView ID="GridView2" runat="server"></asp:GridView>
    </p>
    <p>
        <h2>Example 3 - Date Range</h2>
        <code>
            <asp:Literal ID="Literal3" runat="server" Text="var query = context.GetQueryable<Article>().Where(item => item.PublishDate.Between(DateTime.Now.AddDays(-10), DateTime.Now, Inclusion.Both));"></asp:Literal>
        </code>
        <asp:GridView ID="GridView3" runat="server"></asp:GridView>
    </p>
    <p>
        <h2>Example 4 - Projection</h2>
        <code>
            <asp:Literal ID="Literal4" runat="server" Text="var query = context.GetQueryable<Article>().Where(item => item.Title.StartsWith('Sa')).Select(projection => new { Name = projection.Title, PubDate = projection.PublishDate});"></asp:Literal>
        </code>
        <asp:GridView ID="GridView4" runat="server"></asp:GridView>
    </p>
    <p>
        <h2>Example 5 - GetResults</h2>
        <code>
            <asp:Literal ID="Literal5" runat="server" Text="var query = context.GetQueryable<Article>().Where(item => item.Name == 'News Article');"></asp:Literal>
        </code>

        <h2>Hits</h2>
        <asp:GridView ID="GridView5" runat="server"></asp:GridView>

        <h2>Facets</h2>
        <asp:Literal ID="Facets" runat="server"></asp:Literal>
      
        <h2>Count</h2>
        <asp:Literal ID="Count" runat="server"></asp:Literal>
    </p>

</div>

