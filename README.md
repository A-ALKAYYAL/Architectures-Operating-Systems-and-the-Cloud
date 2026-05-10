# 001_PORT:Portfolio on Architectures, Operating Systems and The Cloud

## Repository Structure

```text
ARCHOSC-Assignment/
│
├── Pictures/                  # All screenshots used across all labs
│
├── index.html                 # Home page / main portfolio entry point
│
├── lab2.html                  # Introduction to GitHub Lab
├── lab3.html                  # Introduction to YAML & GitHub Actions Lab
├── lab4.html                  # HTML, CSS & JavaScript Basics Lab
├── lab5.html                  # Using Virtual Machines Lab
├── lab6.html                  # Virtual Networks & VM Deployment Lab
├── lab7.html                  # OS CPU Scheduling Lab
├── lab8.html                  # OS Memory Management Lab
├── lab9.html                  # OS Resource Allocation (Banker's Algorithm) Lab
│
├── script.js                  # JavaScript functionality (dark mode, navigation, etc.)
├── style.css                  # Styling for the entire portfolio website
│
└── README.md                  # This file - documentation of the repository
```

---

## Lab Descriptions

### Lab 1: ARCHOSC-Introductory-Lab (Introductory Lab)

This first lab introduced essential skills needed throughout the module. I learned about the Computer Science Workspace - a locally stored secure area on lab machines that prevents OneDrive syncing issues with IDEs and game engines. I practiced taking effective screenshots using the Windows shortcut (Windows Key + Shift + S), editing them in Paint, and organizing them into a Pictures folder. I was introduced to the basics of HTML (structure of web pages), CSS (styling), and JavaScript (custom behaviour). I created a simple "Picture Switcher" website with a button that toggles between two images, learning about tags, the head/body structure, linking external CSS/JS files, and using `getElementById` and event listeners. I also learned to push work to GitHub. The lab concluded with tasks to implement multiple pages, a navigation bar, and hosting on GitHub Pages.

---

### Lab 2: ARCHOSC-Introduction-To-GitHub

This lab focused on version control using Git and GitHub. I created a local repository inside the Computer Science Workspace using the terminal (`git init`). I then created a private GitHub repository, linked it to my local repo using `git remote add origin`, and pulled the remote content (`git pull origin main`). I created a basic HTML website displaying my name, tracked files with `git add .`, committed changes with `git commit -m "message"`, and pushed to GitHub using `git push -u origin main`. I also learned to host websites on GitHub Pages by changing repository visibility to public and deploying from the main branch. Finally, I improved the website with custom CSS changes and added a reflection section discussing how version control is used in industry for collaboration, backup, and deployment.

---

### Lab 3: ARCHOSC-Introduction-To-YAML

This lab covered GitHub Actions and YAML pipelines for Continuous Integration and Continuous Deployment (CI/CD). I first fixed the private repository issue by accepting a GitHub Classroom assignment and copying my previous work into the new repository. I then unpublished my GitHub Pages site and switched to GitHub Actions as the deployment source. I created a `deployment.yml` workflow file that: (1) names the pipeline "Deploy Website", (2) triggers on pushes to the main branch, (3) sets appropriate permissions, (4) spins up an Ubuntu VM, (5) checks out the repository, (6) uploads artifacts, and (7) deploys to GitHub Pages. I learned YAML syntax including indentation rules, key-value pairs, and the structure of jobs and steps. I added all lab content to a new HTML page with hyperlinks between pages.

---

### Lab 4: ARCHOSC-HTML-CSS-JavaScript

This lab deepened my understanding of front-end web development. I created external `styles.css` and `script.js` files and linked them to my HTML. I learned to style elements by tag name (e.g., `h1 { color: orange; }`) and by class name (e.g., `.name { color: green; }`). In JavaScript, I used `document.querySelector()` to access elements by their class and logged them to the console. I created a navigation bar (converted from a list of hyperlinks) to browse between individual lab pages. The main task was implementing a Dark Mode toggle: I created a button, added custom CSS classes for dark mode colours, used JavaScript to add an event listener, and toggled the CSS classes when the button was pressed. I also learned to save changes as administrator when editing files in protected folders like `wwwroot`.

---

### Lab 5: ARCHOSC-Using-Virtual-Machines

This lab involved hosting my portfolio on a web server running inside a Virtual Machine. I used Remote Desktop Connection to access two VMs: `STU-XXXXXX-VM1` (Deployment Server) and `STU-XXXXXX-VM2` (Development Server). On each VM, I installed Git, cloned my portfolio repository, and copied files into `C:\inetpub\wwwroot` (the default IIS web server directory). I verified IIS was running using `Get-Service w3svc`. I accessed my live website from my local browser using `http://stu-XXXXXX-vm1.net.dcs.hull.ac.uk`. On the development VM, I installed Visual Studio Code, made a small change to my website, saved with administrator permissions, and tested the updated version. I learned the benefit of having separate development and deployment servers: changes can be tested safely on the development VM before pushing live to the deployment server, preventing broken code from reaching users.

---

### Lab 6: ARCHOSC-VirtualNetworks-VMDeployment

This lab focused on virtual networking and automating deployment using GitHub self-hosted runners. I used `ipconfig` to find the IPv4 addresses of both VMs, then used `ping` to test connectivity between them. I used `Test-NetConnection -port 80` to verify IIS was running and accessible. I then set up a GitHub self-hosted runner on VM2 by downloading and configuring the runner software, running `./run.cmd` to listen for jobs. I created a new YAML pipeline (`PushToVM.yml`) that automatically copies my GitHub repository files to the VM's `wwwroot` folder when changes are pushed. This automated the deployment process: any push to GitHub triggers the runner to pull the latest code and deploy it to IIS. I demonstrated this by updating my website on VM2 and seeing the changes appear automatically on VM1's live site.

---

### Lab 7: OS Scheduling - Exploring CPU Scheduling

This lab introduced CPU scheduling concepts including long-term, medium-term, and short-term schedulers. I implemented:

- **Long-term scheduling**: Filtering jobs based on priority (admitting only jobs with Rank > 5) using C# LINQ (`Where` clause).
- **Short-term scheduling - FCFS (First-Come-First-Serve)**: Sorted processes by arrival time, calculated finish time, turnaround time (Finish - Arrival), and normalized turnaround time (Turnaround / Execution Time). Plotted Gantt charts.
- **Round-Robin scheduling**: Implemented with time quantums TQ = 1, 3, 4, and 6. Calculated the same metrics and observed that smaller time slices increase context switching and turnaround time, while larger slices behave similarly to FCFS.
- **Context switching with priority**: Extended Round-Robin to include priority-based scheduling (using Table 1 data) with TQ = 1 and 6. Priority determined execution order while time quantum controlled run duration. Smaller slices caused frequent context switches (higher overhead); larger slices reduced switching but allowed lower-priority processes to block higher-priority ones longer.

All algorithms were implemented in C# with proper input validation, meaningful variable names, and Gantt chart visualizations.

---

### Lab 8: OS Memory and Memory Management

This lab covered virtual memory, paging, page tables, TLBs, and memory interfacing. Key calculations included:

- **Task 1**: With a 4-bit VPN, number of virtual pages = 2^4 = 16. With an 8-bit offset, page size = 2^8 = 256 bytes. Translated virtual address 0x2C8 to binary, extracted VPN (0x2) and offset (0xC8), and mapped to PPN (0x4) using the page map.
- **Task 2**: Calculated end addresses for page frames (4KB each): Frame 0: 0-4095, Frame 3: 12288-16383, Frame 5: 20480-24575, Frame 7: 28672-32767.
- **Task 3**: From a 32-bit virtual address, extracted the virtual page number (1), page frame value (binary 001, decimal 1), and constructed the physical address by concatenating PPN and offset.
- **Task 4**: Calculated physical memory pages (2^24 / 2^10 = 16,384), page table entries (2^22 = 4,194,304), bits per entry (14 PPN bits + 2 control bits = 16 bits), and page table size (2^23 bytes = 8,192 pages). Simulated a 4-entry TLB with hit/miss tracking.
- **Task 5**: Memory interfacing with decoders - determined 4 address bits for 14 memory locations (2^4 = 16 ≥ 14). For combined address pattern (WR RD EN A3 A2 A1 A0) reading index A (binary 1010), the address value was 0111010 = 0x3A.

---

### Lab 9: OS Resource Allocation - Banker's Algorithm

This lab applied the Banker's Algorithm to determine safe or unsafe states for five processes (P1-P5) with three resource types (R1, R2, R3). I:

1. **Manually analysed the initial state**: With allocation matrix (P1:0,1,0; P2:2,0,0; P3:3,0,2; P4:2,1,1; P5:0,0,2), maximum demands (P1:7,5,4; P2:3,2,2; P3:9,0,2; P4:2,2,2; P5:4,4,4), and available resources (R1=3, R2=3, R3=2). Calculated Need = Max - Allocation, then found safe sequence: P2 → P3 → P4 → P5 → P1.

2. **Increased R1 to 12** (keeping R2=5, R3=7): New safe sequence became P4 → P2 → P3 → P5 → P1.

3. **Further increased R2 to 7** (R1=12, R2=7, R3=7): Safe sequence became P4 → P2 → P3 → P1 → P5.

4. **Reflected** that increasing R2 allowed P1 to execute earlier, creating more scheduling options and reducing bottlenecks.

5. **Implemented in C#**: A program that accepts allocation, maximum need, and available resources as input (or uses hardcoded test values), calculates the need matrix, checks for a safe sequence using the Banker's Algorithm, and outputs the safe execution order or declares an unsafe state.

---

## Reflection

Throughout these nine labs, I have developed practical skills in:

- **Version control** using Git and GitHub for backup, collaboration, and deployment automation.
- **CI/CD pipelines** using GitHub Actions and YAML for automated testing and deployment.
- **Web development** with HTML, CSS, and JavaScript including responsive design and dark mode toggles.
- **Virtualisation** using RDP, VMs, IIS web server configuration, and virtual networking.
- **Operating Systems concepts**: CPU scheduling (FCFS, Round-Robin, priority), memory management (paging, page tables, TLBs), and resource allocation (Banker's Algorithm).
- **C# programming** for simulations and algorithm implementations.

The portfolio serves as both a demonstration of technical competence and a reference for future work in cloud computing, DevOps, and systems programming.