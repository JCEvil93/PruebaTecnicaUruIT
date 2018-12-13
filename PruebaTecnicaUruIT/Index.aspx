<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PruebaTecnicaUruIT.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Game of Drones</title>
    <link rel="icon" href="images/favicon.png" />    
    <link href="styles/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/fontawesome.css" rel="stylesheet" />
    <link href="styles/all.css" rel="stylesheet" />
    <link href="styles/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <i class="fas fa-gamepad" id="icon-game"></i>
            <a class="navbar-brand" href="#">GAME OF DRONES</a>
            <button class="btn btn-outline-success" data-toggle="modal" type="button" id="btnLastWinner" runat="server" data-target="#winnersmodal">Last Winners<i class="fas fa-trophy" id="icon-trophy"></i></button>
        </nav>
        <div class="modal fade" id="winnersmodal" tabindex="-1" role="dialog" aria-labelledby="winnersmodal" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="WinnersModalLabel">Last Winners</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvLastWinners" runat="server"></asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div runat="server" id="divStart" class="card">
            <h2>Enter Player's Names</h2>
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="lblPlayer1" runat="server" Text="Player 1"></asp:Label>
                    </div>
                    <div class="col-6">
                        <asp:TextBox ID="txtPlayer1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Label ID="lblPlayer2" runat="server" Text="Player 2"></asp:Label>
                    </div>
                    <div class="col-6">
                        <asp:TextBox ID="txtPlayer2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" class="btn btn-danger" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <div runat="server" id="divRounds" class="card">
            <h1>Round<asp:Label ID="spRound" runat="server" Text=""></asp:Label></h1>
            <br />
            <h2><span runat="server" id="spPlayer"></span></h2>
            <div class="row">
                <button runat="server" id="btnRock" onserverclick="btnRock_Click" class="btn btn-action">
                    <i class="fas fa-hand-rock"></i>
                </button>
                <button runat="server" id="btnPaper" onserverclick="btnPaper_Click" class="btn btn-action">
                    <i class="fas fa-hand-paper"></i>
                </button>
                <button runat="server" id="btnScissors" onserverclick="btnScissors_Click" class="btn btn-action">
                    <i class="fas fa-hand-scissors"></i>
                </button>
            </div>


        </div>

        <div runat="server" id="divGrid" class="card">
            <asp:GridView ID="gvRoundWinner" runat="server">
            </asp:GridView>
            <br />
            <h1 runat="server" ><span runat="server" id="spWinner"></span></h1>
            <div runat="server" id="iconWinnerTrophy"><i class="fas fa-trophy"></i></div>
            <asp:Button ID="btnPlayAgain" runat="server" Text="PlayAgain" OnClick="btnPlayAgain_Click" class="btn btn-danger" />
        </div>

    </form>

    <script src="scripts/jquery-3.3.1.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
   
</body>
</html>
