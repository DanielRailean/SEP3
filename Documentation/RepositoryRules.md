### 📝 [Repository rules]()
- Any merges are done only into the master. (No merging of master or another branch into yours)
- Always update the project before starting the work
- Commits should have a clear commit message, *"small fixes","small update", "update" and similar* are not viable commit messages.
- Work is done on a separate branch
- Merging a branch to **master** requires one review.


### 💎 [Useful git commands]()

 * *git reset HEAD~* >   **Removes last local commit**

 * *git rm --cached *filename.extension* >* **Removes already tracked file**

 * *git reset --hard *commitId* >* **Resets local branch to the given commitId , usually used together with command below**

 * *git push --force >* **Pushes local branch to remote.**

 * *git rm -r --cached path_to_your_folder/* **Removes recursively an already added directory. Be sure to add the items to .gitignore before this command**
 
 * *git rm --cached path_to_your_file/* **Removes recursively an already added file. Be sure to add the file to .gitignore before this command**

  ### 🎁 [GitHub Tips]()
 1. Finish your functionality before making a pull request to master.
 2. If you want to update your branch , create another one and bring uncommited changes with you. (Commited changes can be undone with 1st useful git command).