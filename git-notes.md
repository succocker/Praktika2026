# Git notes
## Task 15 — Basic git
### Output (git log --graph --oneline --all)
*   207f381 (HEAD -> main) merge: resolve conflict in readme
|\
| * f14253f (feature/git-basics) docs: edit readme in feature
| * 738dce2 docs: unique change in feature
| * 6139c72 docs: unique change in main
| * a0e098b docs: change readme in feature
* | bb09fab docs: edit readme in main
|/
* 2ec481a docs: add readme in main

## Task 46 — git stash
### Commands
git stash
git stash list
git stash pop

### Output (до pop)
(вставьте вывод из шага 3)

### Output (после pop)
(пусто)

### What I learned
Научился прятать незакоммиченные изменения в stash и возвращать их обратно.




## Task 49 — reflog
### Commands
git commit --allow-empty -m "feat: commit to be lost and found"
git reset --hard HEAD~1
git reflog
git reset --hard <hash>

### Output (git reflog)
(Вставьте сюда вывод команды git reflog, где видно ваш потерянный коммит)

### Output (git log после восстановления)
(Вставьте сюда вывод git log --oneline -1 после восстановления)

### What I learned
Научился находить потерянные коммиты через reflog и восстанавливать их с помощью reset --hard.