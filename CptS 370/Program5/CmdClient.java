import java.net.*;
import java.io.*;

public class CmdClient extends Thread{
    // to store port number received from terminal
    private int portnum;
    private String host;

    // Contructor to get server's port number
    public CmdClient(String args[]){
        this.portnum = Integer.parseInt(args[0]);
        if(args.length > 1){
            this.host = args[1];
        }
        else{
            host = "127.0.0.1";
        }
    }

    public void run(){
        if(portnum >= 5000 && portnum <= 5500){
            try {

                /* make connection to server socket */
                Socket sock = new Socket(host, portnum);
                PrintWriter pout = new PrintWriter(sock.getOutputStream(), true);
                BufferedReader bin = new BufferedReader(new InputStreamReader(sock.getInputStream()));

                // Send username to server
                String username = System.getProperty("user.name");
                SysLib.cout("Here is the user name being sent to the server: " + username + "\n");
                pout.println(username);

                //while(bin.readLine() == null){};
                //SysLib.cout("got past the while loop!");

                try{sleep(6000);}
                catch(InterruptedException e){}; 

                // Receive new port to listen
                String lin = bin.readLine();
                SysLib.cout(lin + "is the line read");
                int portnum2 = Integer.parseInt(lin);

                SysLib.cout("Here is the port received from the server" + portnum2);
                portnum2 = Integer.parseInt(lin);
                
                //SysLib.cout(portnum2 + " is the port received");

                // Connect to the port provided by the Server
                Socket sock2 = new Socket(host, portnum2);
                PrintWriter pout2 = new PrintWriter(sock2.getOutputStream(), true);
                BufferedReader bin2 = new BufferedReader(new InputStreamReader(sock2.getInputStream()));

                // use a stringbuilder object to read programs and arguments from the user
                //SysLib.cout("Connection established try writing some text for the server to reverse:\n");

                Boolean userInput = true;
                while(userInput){
                    SysLib.cout("Please enter text to reverse: ");
                    StringBuffer sb = new StringBuffer();
                    SysLib.cin(sb);

                    // Store user input in a string
                    String msg = new String(sb);
            
                    /* write the Text to the socket */
                    pout2.println(msg);

                    /* read the date from the socket */
                    String line = bin2.readLine();
                    System.out.println(line);

                    // If the message is bye or die then close the connection
                    if(msg.equals("bye") || msg.equals("die")) { userInput = false; }
                }

                // Close the second socket connection
                bin2.close();
                pout2.close();
                sock2.close();

                /* close the socket connection*/
                bin.close();
                pout.close();
                sock.close();

                // exit the shell
                SysLib.exit();
            }
            catch (IOException ioe) {
                System.err.println(ioe);
            }
        }
        else{
            SysLib.cout("oop port num is out of bounds");
            SysLib.exit();
        }
    }
}
