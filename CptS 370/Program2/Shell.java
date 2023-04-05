/**
 * Sean Skinner
 * CptS 370
 * Program 2
 */

import java.util.ArrayList;
import java.util.List;

public class Shell extends Thread{
    public Shell() {
    }

    // Method that interprets user input and spawns threads to execute commands.
    public void runner2(String arguments){

        // ID holder for thread being created/joined.
        int id;

        // List to hold the created threads for given command.
        List<Integer> threads = new ArrayList<Integer>();

        // Split string into separate commands to be executed concurrently.
        String[] cmds = arguments.split("&");

        // prepare each command for a thread.
        for(int i = 0; i < cmds.length; i++){
            // Use SysLibs stringtoArgs to convert each string into a command + arguments.
            String[] args = SysLib.stringToArgs(cmds[i]);

            // If command is not null then execute the command.
            if(args.length > 0){

                // Assign command to a thread, then append list with thread id to keep track of current threads.
                id = SysLib.exec(args);
                threads.add(id);
            }
        }

        // While list of thread is not empty have parent threads wait for children.
        while(threads.size() != 0){

            // wait for child thread, when joined remove child thread from list of threads.
            id = SysLib.join();
            if(threads.contains(id)){

                // find index of thread then remove it from the list.
                threads.remove(threads.indexOf(id));
            }
        }
    }

    // Run method that initializes commands to be given ro runner2 method.
    public void run() {
        // variable to hold line number.
        int num = 1;

        // variable to control while loop.
        boolean exit = true;

        // continue to execute this code to run programs in thread OS until "exit" is read.
        while(exit){

            // print out line number
            SysLib.cout("Shell [" + num + "]");

            // use a stringbuilder object to read programs and arguments from the user
            StringBuffer sb = new StringBuffer();
            SysLib.cin(sb);

            // Store user input in a string
            String userinput = new String(sb);

            String[] cmds = userinput.split(";");

            // If exit is input then set 'exit' bool to false and break the loop
            if(userinput.equals("exit")){
                exit = false;
                break;
            } 

            // Check to make sure input is not null
            if(!userinput.isEmpty()){

                // If not null increment the line number
                num++;

                // Split the commands by ; to find out which commands to run concurrently and which commands should wait
                cmds = userinput.split(";");

                // Send commands joined by & together, commands split by ; must wait.
                for(int i = 0; i < cmds.length; i++){
                    runner2(cmds[i]);
                }
            }
        }
        // exit the Shell
        SysLib.exit();
    }
}

