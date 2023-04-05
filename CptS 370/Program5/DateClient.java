import java.net.*;
import java.io.*;

public class DateClient extends Thread
{
    // public static void main(String[] args) {
    //     try {
    //         /* make connection to server socket */
    //         Socket sock = new Socket("127.0.0.1",6013);

    //         InputStream in = sock.getInputStream();
    //         BufferedReader bin = new
    //             BufferedReader(new InputStreamReader(in));

    //         /* read the date from the socket */
    //         String line;
    //         while ( (line = bin.readLine()) != null)
    //             System.out.println(line);

    //         /* close the socket connection*/
    //         sock.close();
    //     }
    //     catch (IOException ioe) {
    //         System.err.println(ioe);
    //     }
    // }

    // to store port number received from terminal
    private int portnum;

    // Contructor to get server's port number
    public DateClient(String args[]){
        this.portnum = Integer.parseInt(args[0]);
    }

    public void run(){
        try {
            /* make connection to server socket */
            Socket sock = new Socket("127.0.0.1", portnum);

            InputStream in = sock.getInputStream();
            BufferedReader bin = new
                BufferedReader(new InputStreamReader(in));

            /* read the date from the socket */
            String line;
            while ( (line = bin.readLine()) != null)
                System.out.println(line);

            /* close the socket connection*/
            sock.close();

            // exit the shell
            SysLib.exit();
        }
        catch (IOException ioe) {
            System.err.println(ioe);
        }
    }
}