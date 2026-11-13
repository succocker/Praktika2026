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

## Task 47 — git rebase -i
### Log before squash
(вставьте вывод из Шага 2)

### Log after squash
(вставьте вывод из Шага 5)

### What I learned
Научился объединять несколько мелких коммитов в один с помощью интерактивного rebase.

## Task 48 — tags
### Commands
git tag -a v1.0.0 -m "Release v1.0.0"
git push origin v1.0.0
git tag
git show v1.0.0

### Output (git tag)
v1.0.0

### Output (git show v1.0.0)
(Вставьте сюда вывод команды git show)

### What I learned
Научился создавать аннотированные теги для релизов и отправлять их на удаленный сервер.




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



## Task 50 — git bisect
### Commands
git bisect start
git bisect bad
git bisect good <hash>
git bisect reset

### Output
(Вставьте сюда строчку, которую Git вывел при нахождении плохого коммита, например: "<hash> is the first bad commit")

### What I learned
Научился использовать git bisect для бинарного поиска сломанного коммита в истории.
## Task 35 — Branches & PR
### PR link
https://github.com/succocker/Praktika2026/pull/1

### Rebase result
6d25e9a (HEAD -> feature/rebase-demo) feat: complete tasks 16-26 (level 2)

