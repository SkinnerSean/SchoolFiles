#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include <sys/wait.h>

int main(int argc, char* argv[]){
	int fd[2];
	if(pipe(fd) == -1){
		return 1;
	}

	int fd2[2];
	if(pipe(fd2) == -1){
		return 1;
	}
	
	int status = 0;

	int child, gchild, ggchild;

	child = fork();
	if(child == -1){
		printf("An error occurred with child fork\n");
		return 2;
	}

	if(child == 0){
		gchild = fork();
		if(gchild == -1){
			printf("An error occurred with grandchild fork\n");
			return 3;
		}

		if(gchild == 0){
			
			ggchild = fork();
			if(ggchild == -1){
				printf("An error occurred with great grandchild fork\n");
			}

			// Great Grandchild code
			if(ggchild == 0){
				close(fd2[0]);
				close(fd2[1]);
				//printf("hello from ggchild!\n");
				dup2(fd[1], STDOUT_FILENO);
				close(fd[0]);
				close(fd[1]);
				execlp("ps", "ps", "-A", NULL);
				exit(0);
			}

			//Grandchild's code (Grep argv[1])
			else{
				pid_t ggpid = wait(NULL);
				//printf("ggchild finished execution its id was: %d\n", ggpid);
				//printf("hello from gchild\n");
				dup2(fd[0], STDIN_FILENO);
				//printf("first dup\n");
				dup2(fd2[1], STDOUT_FILENO);
				//printf("second dup");
				close(fd[0]);
				close(fd[1]);
				close(fd2[0]);
				close(fd2[1]);
				//printf("trying to execute grep\n");
				execlp("grep", "grep", argv[1], NULL);
				//printf("successful grep now exiting");	
				exit(1);
			}
		}

		//Child's code
		else{
			close(fd[1]);
			close(fd[0]);
			close(fd2[1]);
			//printf("waiting for gchild");
			pid_t gpid = wait(NULL);
			//printf("Hello from child");
			dup2(fd2[0],STDIN_FILENO);
			close(fd2[0]);
			execlp("wc", "wc", "-l", NULL);
			exit(1);
		}
		
		
	}

	//Parent's code here
	else{
		//pid_t cpid = wait(NULL);
		close(fd[0]);
		close(fd[1]);
		close(fd2[0]);
		close(fd2[1]);
		//printf("Hello from parent");
	}

	return 0;

}
