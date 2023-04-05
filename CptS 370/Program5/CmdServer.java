import java.net.*;
import java.io.*;

public class CmdServer extends Thread {

    // function to reverse a string
    public String reverseText(String text){
        char letter;
        String revText = "";
        for(int i = 0; i < text.length(); i++){
            letter = text.charAt(i);
            revText = letter + revText;
        }
        return revText;
    }

    // function to check if the port is available.
    public boolean portAvailable(int port){
        ServerSocket s = null;

        // check if the port is not in use
        try{
            s = new ServerSocket(port);

            // allows the port to forgo its timeout state
            s.setReuseAddress(true);
            return true;
        }
        catch(IOException e){ }

        // close the socket at the end
        finally{
            if(s != null){
                try{ s.close(); }
                catch(IOException e1){ }
            }
        }

        // if the port is in use return false.
        return false;
    }

    public void run(){
        try {
            for(int i = 5000; i <= 5500; i++){

                // First check if the port is available, if not then move to next port number.
                if(portAvailable(i)){
                    ServerSocket sock = new ServerSocket(i);

                    // print out port and host for client
                    SysLib.cout(String.format("%s is listening on port %d\n", InetAddress.getLocalHost().getHostName(), sock.getLocalPort()));
            
                    boolean outloop = true;

                    /* now listen for connections */
                    while (outloop) {
                        Socket client = sock.accept();
                        PrintWriter pout = new PrintWriter(client.getOutputStream(), true);
                        BufferedReader bin = new BufferedReader(new InputStreamReader(client.getInputStream()));

                        String username = System.getProperty("user.name");
                        String clientname;

                        clientname = bin.readLine();
                        // SysLib.cout("Client username = " + clientname);
                        // SysLib.cout("Server username = " + username);

                        // Check if the username matches if not close everything and exit.
                        if(!username.equals(clientname)){
                            SysLib.cout("Usernames dont line up exiting!");
                            pout.close();
                            bin.close();
                            client.close();
                            SysLib.exit();
                        }

                        // username is correct so open a new port and send to user
                        ServerSocket newsock = new ServerSocket(0);
                        SysLib.cout(String.format("%s is listening on port %d\n", InetAddress.getLocalHost().getHostName(), newsock.getLocalPort()));
                        int newsockport = newsock.getLocalPort();
                        //SysLib.cout("Sending the number " + newsockport + " over the socket to client \n");
                        pout.println(newsockport);

                        // inner while loop for new socket
                        boolean midloop = true;
                        while(midloop){
                            Socket client2 = newsock.accept();
                            PrintWriter pout2 = new PrintWriter(client2.getOutputStream(), true);
                            BufferedReader bin2 = new BufferedReader(new InputStreamReader(client2.getInputStream()));

                            while(true){
                                String line;
                                String revLine;

                                // Get line to reverse from the client
                                line = bin2.readLine();

                                // Check if the line is equal to bye;
                                if(line == "bye"){ 
                                    break;
                                }
                                if(line == "die"){
                                    break;
                                }

                                revLine = reverseText(line);
                                pout2.println(revLine);
                            }

                            pout2.close();
                            bin2.close();
                            client2.close();
                            break;
                        }
            
                        /* close the socket and resume */
                        /* listening for connections */
                        pout.close();
                        bin.close();
                        client.close();
                    }
                }
            }
        }
        catch (IOException ioe) {
            System.err.println(ioe);
        }
    }
}