import java.net.*;
import java.io.*;
public class DateServer extends Thread
{
    // public static void main(String[] args) {
    //     try {
    //     ServerSocket sock = new ServerSocket(6013);

    //     /* now listen for connections */
    //     while (true) {
    //         Socket client = sock.accept();

    //         PrintWriter pout = new
    //         PrintWriter(client.getOutputStream(), true);

    //         /* write the Date to the socket */
    //         pout.println(new java.util.Date().toString());

    //         /* close the socket and resume */
    //         /* listening for connections */
    //         client.close();
    //         }
    //     }
    //     catch (IOException ioe) {
    //         System.err.println(ioe);
    //     }
    // }

    public void run(){
        try {
            ServerSocket sock = new ServerSocket(0);

            // print out port for client
            SysLib.cout("port number DateServer is using: " +  sock.getLocalPort());
    
            /* now listen for connections */
            while (true) {
                Socket client = sock.accept();
    
                PrintWriter pout = new
                PrintWriter(client.getOutputStream(), true);
    
                /* write the Date to the socket */
                pout.println(new java.util.Date().toString());

                // SysLib.cout(String.format("%s is listening on port %d\n", InetAddress.getLocalHost().getHostName(), sock.getLocalPort()));

    
                /* close the socket and resume */
                /* listening for connections */
                client.close();
                }
            }
            catch (IOException ioe) {
                System.err.println(ioe);
            }
    }
}