del _site /s /q

rem Documentation spell check
powershell -ExecutionPolicy ByPass -NoProfile -Command "& { Push-Location ..; ./build.ps1 --target SpellCheck --no-logo; }"

rem For local testing, unset baseUrl in _config.yml
bundle exec jekyll serve --incremental